using System.Collections;
using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace GorillaServerStats
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.thaterror404.gorillatag.SererStats", "ServerStats", "1.0.7")]

    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        bool inRoom;
        public GameObject _forestSign;
        public TextMeshPro _signText;
        public bool _init;
        public int _tags = 0;
        public int _ptimer = 20;
        public int totalCodesJoined = 1;
        public int _Tagged;
        public bool _isKeyCloned = false;

        Coroutine timerCoroutine, PageCoroutine;
        System.TimeSpan time = System.TimeSpan.FromSeconds(0);
        string playTime = "00:00:00";
        public bool screen1 = true;
        string PlayerID;
        public int mycosmeticcost;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                timerCoroutine = StartCoroutine(Timer());
                PageCoroutine = StartCoroutine(PageSwap());
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
        }
        public string boardStatsUpdate()
        {
            if (PhotonNetwork.CurrentRoom == null)
            {
                return "Welcome To Gorilla Stats!" +
                "\r\n\nOriginal By: 3rr0r" +
                "\r\nFixed By: E14O" +
                "\r\n\nPlease join a room for stats to appear!";
            }
            else
            {
                var lobbyCode = PhotonNetwork.CurrentRoom.Name;
                int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
                var master = PhotonNetwork.MasterClient;
                int totalPlayerCount = PhotonNetwork.CountOfPlayersInRooms;
                // int ShinyRockCount =
                // int CosmeticCount = ;
                // int AccountValue =
                Color ColourCode = GorillaTagger.Instance.offlineVRRig.playerColor;
                int R = Mathf.RoundToInt(ColourCode.r * 9f);
                int G = Mathf.RoundToInt(ColourCode.g * 9f);
                int B = Mathf.RoundToInt(ColourCode.b * 9f);
                if (screen1)
                {
                    return "LOBBY CODE: " + lobbyCode + "                   " + _ptimer +
                    "\r\nTOTAL LOBBYS: " + totalCodesJoined +
                    "\r\nPLAYERS: " + playerCount +
                    "\r\nMASTER: " + master.NickName +
                    "\r\nACTIVE PLAYERS: " + totalPlayerCount +
                    "\r\nPLAY TIME: " + playTime +
                    "\r\nPING: " + PhotonNetwork.GetPing();
                }
                else
                {
                    return "Player Colour: " + "R:" + R + " G:" + G + " B:" + B + "                 " + _ptimer +
                    "\r\nShiny Rock Count: ERROR NOT ADDED YET" +
                    "\r\nCosmetic Count: ERROR NOT ADDED YET" + 
                    "\r\nAccount Value: ERROR NOT ADDED YET" +
                    "\r\nPlayer ID: ERROR NOT ADDED YET";
                }
            }
        }

        void OnGameInitialized()
        {
            
            _init = true;
            _forestSign = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/ScreenText (1)");
            if (_forestSign == null)
            {
                Debug.LogError("[ServerStats] Could not find ForestSign");
                return;
            }
            _signText = _forestSign.GetComponent<TextMeshPro>();
            if (_signText == null)
            {
                Debug.LogError("[ServerStats] Could not find Text component for ForestSign");
                return;
            }
            if (PhotonNetwork.CurrentRoom == null)
            {
                Debug.LogError("[ServerStats] Current room is null");
                return;
            }
            else
            {
                _signText.text = boardStatsUpdate();
            }
        }

        void Update()
        {
            if (_forestSign != null)
            {
                _signText = _forestSign.GetComponent<TextMeshPro>();
                _signText.text = boardStatsUpdate();
            }
            else
            {
                Debug.Log("[ServerStats] forestSign doesn't exist in Update");
            }
        }

        public void OnJoin()
        {
            inRoom = true;
            totalCodesJoined++;

            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }

            if (_forestSign != null)
            {
                _signText = _forestSign.GetComponent<TextMeshPro>();
                _signText.text = boardStatsUpdate();
            }
            else
            {
                Debug.Log("[ServerStats] forestSign doesn't exist in OnJoin");
            }
        }

        public void OnLeave()
        {
            inRoom = false;

            if (_forestSign != null)
            {
                _signText = _forestSign.GetComponent<TextMeshPro>();
                _signText.text = "Welcome To Gorilla Tag!\r\n\r\nPlease Join A Room For Stats To Appear!\r\n\rYou Have Been Playing For: " + playTime;
            }
        }
        IEnumerator PageSwap()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (_ptimer < 21 && _ptimer > 0)
                {
                    _ptimer--;
                }
                else if (_ptimer <= 0)
                {
                    _ptimer = 20;
                    if (screen1 == false)
                    {
                        screen1 = true;
                    }
                    else if(screen1 == true)
                    {
                        screen1 = false;
                    }

                }

                _signText.text = boardStatsUpdate();
            }
        }
        IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                time = time.Add(System.TimeSpan.FromSeconds(1));
                playTime = time.ToString(@"hh\:mm\:ss");

                if (_forestSign != null)
                {
                    _signText = _forestSign.GetComponent<TextMeshPro>();
                    _signText.text = boardStatsUpdate();
                }
                else
                {
                    Debug.LogWarning("[ServerStats] forestSign not found in Timer Coroutine.");
                }

            }
        }
    }
}