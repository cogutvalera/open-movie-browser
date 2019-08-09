using UnityEngine;

namespace Canada.Test
{
    [System.Serializable]
    public class OpenMovie
    {
        public string Title;
        public int Year;
        public string imdbID;
        public string Type;
        public string Poster;
    }

    [System.Serializable]
    public class DetailedOpenMovie : OpenMovie
    {
        public string Rated;
        public string Released;
        public string Runtime;
        public string Genre;
        public string Director;
        public string Writer;
        public string Actors;
        public string Plot;
        public string Language;
        public string Country;
        public string Awards;
        public RatingsOpenMovie[] Ratings;
        public int Metascore;
        public float imdbrating;
        public int imdbVotes;
        public string DVD;
        public string BoxOffice;
        public string Production;
        public string Website;
        public bool Response;

        public static DetailedOpenMovie CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<DetailedOpenMovie>(jsonString);
        }
    }

    [System.Serializable]
    public class RatingsOpenMovie
    {
        public string Source;
        public string Value;
    }

    [System.Serializable]
    public class SearchOpenMovie
    {
        public OpenMovie[] Search;
        public bool Response;

        public static SearchOpenMovie CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<SearchOpenMovie>(jsonString);
        }
    }
}
