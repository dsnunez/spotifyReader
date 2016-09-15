namespace SpotifyMetadata.ResponseModels.Common
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#followers-object
    /// </summary>
    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }
}
