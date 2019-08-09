using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Canada.Test
{
    public class OpenMovieItem : MonoBehaviour
    {
        public Image img;
        public Text txt;

        private OpenMovie om_;

        public void Init(OpenMovie om)
        {
            om_ = om;

            txt.text = "Title: <color=white>" + om.Title + "</color>\n";
            txt.text += "Year: <color=red>" + om.Year + "</color>";

            StartCoroutine(GetPoster(om.Poster));
        }

        public void Init(DetailedOpenMovie dom)
        {
            om_ = null;

            txt.text = "Title: <color=white>" + dom.Title + "</color>\n";
            txt.text += "Year: <color=red>" + dom.Year + "</color>\n";
            txt.text += "Genre: <color=white>" + dom.Genre + "</color>\n";
            txt.text += "Language: <color=red>" + dom.Language + "</color>\n";
            txt.text += "Country: <color=white>" + dom.Country + "</color>\n";
            txt.text += "Director: <color=red>" + dom.Director + "</color>\n";
            txt.text += "Awards: <color=white>" + dom.Awards + "</color>\n";
            txt.text += "Actors: <color=red>" + dom.Actors + "</color>\n";

            StartCoroutine(GetPoster(dom.Poster));
        }

        IEnumerator GetPoster(string urlImg)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(urlImg))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    img.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                }
            }
        }

        public void GetDetailedOpenMovie()
        {
            if (null == om_)
                return;

            OpenMovieBrowser.Instance.DetailedAction(om_.imdbID);
        }
    }
}
