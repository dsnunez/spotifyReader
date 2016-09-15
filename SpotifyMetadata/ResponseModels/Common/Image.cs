namespace SpotifyMetadata.ResponseModels.Common
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#image-object
    /// </summary>
    public class Image
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }
}
