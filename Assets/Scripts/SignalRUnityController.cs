using SignalR.Client._20.Hubs;
using UnityEngine;

public class SignalRUnityController : MonoBehaviour {

    public bool useSignalR = true;
    public string signalRUrl = "http://localhost:5225/";

    private HubConnection _hubConnection = null;
    private IHubProxy _hubProxy;
    private Subscription _subscription;

    void Start()
    {
        if (useSignalR)
            StartSignalR();
    }


    void StartSignalR()
    {
        if (_hubConnection == null)
        {
            _hubConnection = new HubConnection(signalRUrl);

            _hubProxy = _hubConnection.CreateProxy("SignalRSampleHub");
            _subscription = _hubProxy.Subscribe("broadcastMessage");
            _subscription.Data += data =>
            {
                Debug.Log("signalR called us back");
            };
            _hubConnection.Start();
        }
        else
            Debug.Log("Signalr already connected...");

    }

    public void Send(string method, string message)
    {
        if (!useSignalR)
            return;

        var json = "{" + string.Format("\"action\": \"{0}\", \"value\": {1}", method, message) + "}";
        _hubProxy.Invoke("Send", "UnityClient", json);

    }

}

