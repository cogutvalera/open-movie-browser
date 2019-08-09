using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

namespace Canada.Test
{
    public class OpenMovieBrowser : MonoBehaviour
    {
        public string apiUrl = "http://www.omdbapi.com/";
        public string apiKey = "6367da97";

        public InputField query, year;

        public OpenMovieItem omiPrefab;
        public Transform contentRoot;

        private static OpenMovieBrowser m_instance;
        public static OpenMovieBrowser Instance
        {
            get
            {
                return m_instance;
            }
        }

        private void Awake()
        {
            m_instance = this;
        }

        public void DetailedAction(string id)
        {
            StartCoroutine(GetJsonData(OnDetailedResult, BaseUrl() + AddUrlParam("i", id)));
        }

        public void SearchAction()
        {
            if (string.IsNullOrEmpty(query.text))
                return;

            string url = BaseUrl() + AddUrlParam("s", query.text);

            if (!string.IsNullOrEmpty(year.text))
                url += AddUrlParam("y", year.text);

            StartCoroutine(GetJsonData(OnSearchResult, url));
        }

        void EmptyContent()
        {
            while (contentRoot.childCount > 0)
            {
                DestroyImmediate(contentRoot.GetChild(0).gameObject);
            }
        }

        public string BaseUrl()
        {
            return apiUrl + "?apiKey=" + apiKey;
        }

        public string AddUrlParam(string paramName, string paramValue)
        {
            return "&" + paramName + "=" + paramValue;
        }

        public void OnSearchResult(string rez)
        {
            EmptyContent();

            SearchOpenMovie som = SearchOpenMovie.CreateFromJSON(rez);

            if (!som.Response)
                return;

            foreach (OpenMovie om in som.Search)
            {
                OpenMovieItem omi = GameObject.Instantiate<OpenMovieItem>(omiPrefab, contentRoot);
                omi.Init(om);
            }
        }

        public void OnDetailedResult(string rez)
        {
            EmptyContent();

            DetailedOpenMovie dom = DetailedOpenMovie.CreateFromJSON(rez);

            if (!dom.Response)
                return;

            OpenMovieItem omi = GameObject.Instantiate<OpenMovieItem>(omiPrefab, contentRoot);
            omi.Init(dom);
        }

        public IEnumerator GetJsonData(System.Action<string> callback, string url)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (null != callback)
                        callback.Invoke(www.downloadHandler.text);
                }
            }
        }
    }
}
