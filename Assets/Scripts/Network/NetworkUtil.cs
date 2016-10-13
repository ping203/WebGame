using System.Threading;
using System;
using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class NetworkUtil : MonoBehaviour {
    private MessageHandler messageHandler;
    public bool connected;
    protected static NetworkUtil instance;
    private Message message;
    WebSocket w_socket = null;
    ManualResetEvent _clientDone = new ManualResetEvent(false);
    SocketAsyncEventArgs EventArgSend;
    SocketAsyncEventArgs EventArgRead;
    SocketAsyncEventArgs EventArgConnect;
    protected Thread connectThread;
    protected System.Threading.Thread receiveThread;
    private int maxRetry = 1;

    void Awake() {
        if (instance == null) {
            //If I am the first instance, make me the Singleton
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != instance)
                Destroy(this.gameObject);
        }
    }
    string m_url = "";
    public IEnumerator Start() {
        if (m_url.Equals("")) {
            WWW www = new WWW("http://choibaidoithuong.org/config");
            yield return www;
            m_url = www.text;
        }
#if UNITY_WEBGL
        Application.ExternalCall("StartLoad");
#endif
        yield return StartCoroutine(doConnect());
        yield return StartCoroutine(threadReceiveMSG());

    }

    public static NetworkUtil GI() {
        if (instance == null) {
            instance = new NetworkUtil();
        }
        return instance;
    }

    public bool isConnected() {
        return connected;
    }

    public void registerHandler(MessageHandler messageHandler) {
        this.messageHandler = messageHandler;
    }

    public IEnumerator doConnect() {
        if (!connected) {
            // Debug.Log("========try connect=========== : " + Res.IP);
            //w_socket = new WebSocket(new Uri("ws://" + Res.IP + ":" + Res.PORT));        
            //try {
            if (m_url.Equals("")) {
                m_url = "ws://" + Res.IP + ":" + Res.PORT;
            }
            w_socket = new WebSocket(new Uri(m_url));
            // w_socket = new WebSocket(new Uri(Res.IP + ":" + Res.PORT));
            //w_socket = new WebSocket(new Uri("ws://" + Res.IP + ":" + Res.PORT));
            //} catch (Exception e) {
            //    Debug.LogException(e);
            //}
            yield return StartCoroutine(w_socket.Connect());
            connected = true;
            //SceneManager.LoadSceneAsync("main");
        }
    }


    public void sendMessage(Message msg) {
        try {
            byte[] bytes = msg.toByteArray();
            //string message = System.Text.Encoding.UTF8.GetString(bytes);

            #if UNITY_EDITOR
            Debug.Log("Send : " + msg.command);
            #endif
            w_socket.Send(bytes);
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
    }

    private void processMsgFromData(sbyte[] data, int range) {
        sbyte command = 0;
        int count = 0;
        int size = 0;
        try {
            if (range <= 0)
                return;
            Message msg;
            do {
                command = data[count];
                count++;
                sbyte a1 = data[count];
                count++;
                sbyte a2 = data[count];
                count++;
                size = ((a1 & 0xff) << 8) | (a2 & 0xff);
                byte[] subdata = new byte[size];
                #if UNITY_EDITOR
                Debug.Log("Read == " + command);
                #endif
                Buffer.BlockCopy(data, count, subdata, 0, size);
                count += size;
                msg = new Message(command, subdata);
                messageHandler.processMessage(msg);
            } while (count < range);
        } catch (Exception ex) {
            Debug.LogException(ex);
            messageHandler.onDisconnected();
        }
    }

    public void close() {
#if UNITY_EDITOR
        Debug.Log("Close current socket!");
#endif
        cleanNetwork();
    }

    public void cleanNetwork() {
        try {
            connected = false;
            if (w_socket != null) {
                try {
                    w_socket.Close();
                } catch (SocketException ex) {
                    Debug.LogException(ex);
                }

            }
            if (EventArgRead != null) {
                EventArgRead.Dispose();
            }
            if (EventArgSend != null) {
                EventArgSend.Dispose();
            }
            if (EventArgConnect != null) {
                EventArgConnect.Dispose();
            }
            maxRetry = 1;
            connectThread = null;
            _clientDone.Close();
            _clientDone = new ManualResetEvent(false);
        } catch (Exception e) {
            Debug.LogException(e);
        } finally {
            if (connectThread != null && connectThread.IsAlive) {
                connectThread.Abort();
            }
        }
    }

    public void resume(bool pausestatus) {

    }
    public IEnumerator threadReceiveMSG() {
        while (connected) {
            try {
                byte[] data = w_socket.Recv();
                if (data != null) {
                    sbyte[] sdata = new sbyte[data.Length];
                    for (int i = 0; i < data.Length; i++) {
                        if (data[0] > 127) {
                            sdata[0] = (sbyte)(data[0] - 256);
                        }
                        sdata[i] = (sbyte)data[i];
                    }
                    //				string decodedString = Encoding.UTF8.GetString(data);
                    //				byte[] bytes = Encoding.ASCII.GetBytes(decodedString);

                    processMsgFromData(sdata, sdata.Length);
                }
            } catch (Exception e) {
                Debug.LogException(e);
            }
            if (w_socket.error != null) {
                messageHandler.onDisconnected();
                break;
            }
            yield return 0;
        }
        cleanNetwork();
    }

    void OnApplicationQuit() {
        close();
    }
}
