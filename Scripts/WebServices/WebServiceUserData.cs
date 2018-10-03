using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace WebServices 
{
    // Breed refer to the description of a single Fluffy Flaffy in the database service
    [System.Serializable]
    public class Breed
    {
        public string breedCode;
        public string breedName;
        public string description;
        public string places;
        // The owned and capturedAt fields ain't fields in the database service, but I use it here to mantain a single structure.
        public bool owned = false;
        public DateTime capturedAt;
    }

    /*
        This class retrieves the collection of Fluffies Flaffies from a Web Services based on the current user logged in.
        The Web Service is documented in: https://docs.google.com/document/d/1ucY_72iqu3r6lkraEv8ddql4EvQqjjLoEj8ReN2GOfU/edit#
    */
    public class WebServiceUserData : MonoBehaviour
    {
        public delegate void CollectionReceivedHandler();
        public Dictionary<string, Breed> BreedsIndexed = new Dictionary<string, Breed>();
        // Ernesto Simionato's Fluffies Web Server Address
        [SerializeField]
        string serverProductionURL = "http://api.mundogaturro.com/bubble";
        [SerializeField]
        string serverDevlopURL = "http://api.devlop.mundogaturro.cmd.com.ar/bubble";
        [SerializeField]
        bool connectToDevlop = true;
        [SerializeField]
        ExternalInterface externalInterface;
        [SerializeField]
        PopUp serverFailPopUp;
        [Header("Authentication")]
        [SerializeField]
        string username = "";
        [SerializeField]
        string accessTokenFluffie = "";
        string serverURL;
        CollectionReceivedHandler onCollectionReceived;
        delegate void RequestHandler(string response);

        #region Fluffy Data Classes for JSON Serialization
        #pragma warning disable 0649

        [System.Serializable]
        class AuthenticateRequest
        {
            public bool rememberMe;
            public string tokenPocket;
            public string username;
        }

        [System.Serializable]
        class AuthenticateResponse
        {
            public string id_token;
        }

        // This is a simplification of a structure that describes the fluffy flaffys that owns an account
        [System.Serializable]
        class PetOwned
        {
            public Breed breed;
            public string createdAt;
        }

        #pragma warning restore 0649
        #endregion

        public void Init(CollectionReceivedHandler handler)
        {
            onCollectionReceived = handler;
            bool isManualAuthentication = Application.isEditor || Debug.isDebugBuild;
            // This should be done in PetCollectionLoader but I don't have time to fix it now
            if (Application.isEditor || Debug.isDebugBuild)
            {
                serverURL = connectToDevlop ? serverDevlopURL : serverProductionURL;
            }
            else
            {
                serverURL = externalInterface.IsDevlop ? serverDevlopURL : serverProductionURL;
            }

            if (isManualAuthentication)
            {
                // Authenticate();
                RequestFluffyDescriptions();
            }
            else
            {
                username = externalInterface.Username;
                if (!username.Equals(" "))
                {
                    // Connect to server with user external interface values
                    accessTokenFluffie = externalInterface.Token;
                    RequestFluffyDescriptions();
                }
                else
                {
                    // Report error
                    Debug.LogError("Bubble Flanys's error: No se recibieron los parámetros del usuario.");
                }
            }
        }

        void OnAuthenticate(string response)
        {
            AuthenticateResponse authenticateResponse = JsonUtility.FromJson<AuthenticateResponse>(response);
            accessTokenFluffie = authenticateResponse.id_token;
            RequestFluffyDescriptions();
        }

        void RequestFluffyDescriptions()
        {
            Debug.Log("# Init Bubble Flanys Request");
            Debug.Log("- username: "+username);
            Debug.Log("- token: "+accessTokenFluffie);

            var request = new UnityWebRequest(serverURL + "/bubble-breeds", UnityWebRequest.kHttpVerbGET);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Authorization", "Bearer "+accessTokenFluffie);
            StartCoroutine(SendRequest(request, OnReceiveFluffyDescription)); 
        }

        void OnReceiveFluffyDescription(string response)
        {
            if (BreedsIndexed.Count == 0)
            {
                Breed[] breeds = JsonHelper.FromJson<Breed>(response);
                foreach (Breed breed in breeds)
                {
                    BreedsIndexed.Add(breed.breedCode, breed);
                }
            }
            RequestPetCollection();
        }

        void RequestPetCollection()
        {
            var request = new UnityWebRequest(serverURL + "/gaturro/" + username + "/bubble-pets", UnityWebRequest.kHttpVerbGET);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Authorization", "Bearer "+accessTokenFluffie);
            StartCoroutine(SendRequest(request, OnReceivePetCollection));
        }

        void OnReceivePetCollection(string response)
        {
            PetOwned[] petsOwned = JsonHelper.FromJson<PetOwned>(response);
            foreach (PetOwned petOwned in petsOwned)
            {
                Breed petIndexed = BreedsIndexed[petOwned.breed.breedCode];
                petIndexed.owned = true;
                petIndexed.capturedAt = string.IsNullOrEmpty(petOwned.createdAt) ? DateTime.MinValue : Convert.ToDateTime(petOwned.createdAt);
            }
            onCollectionReceived();
        }

        IEnumerator SendRequest(UnityWebRequest request, RequestHandler handler)
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.LogError("Something went wrong, and returned error: " + request.error);
                yield return new WaitForSeconds(0.4f);
                serverFailPopUp.Activate();
                Analytics.instance.ShowError();
            }
            else if (request.responseCode != 200)
            {
                string response = request.downloadHandler.text;
                Debug.Log("* Server connection failed");
                Debug.Log("PostRequest response: " + response);
                Debug.Log("Response code: " + request.responseCode);
                yield return new WaitForSeconds(0.4f);
                if (request.responseCode == 400 && response.Contains("No existe el username"))
                {
                    yield return null;
                    handler("[]");
                }
                else
                {
                    serverFailPopUp.Activate();
                    Analytics.instance.ShowError();
                }
            }
            else
            {
                yield return null;
                handler(request.downloadHandler.text);
            }
        }
    }
}