namespace MyLearning.Chapter05
{
    public class Service
    {
        private readonly List<Track?> _data = [
            new("J'ai Envie De Toi", "Gaia", "Armind", 2010),
            new("Blue Fear", "Armin van Buuren", "Armind", 2003),
            new("Aisha", "Gaia", "Armind", 2010)
        ];

        public IEnumerable<Track> GetAllTracks() => _data.Where(t => t is not null).Select(t => t!);

        public Track? GetTrack(int id) => id >= 0 && id < _data.Count ? _data[id] : null;

        public int AddTrack(Track track)
        {
            _data.Add(track);
            return _data.Count - 1;
        }

        public bool UpdateTrack(int id, Track track)
        {
            if (id < 0 || id >= _data.Count || _data[id] is null) return false;
            _data[id] = track;
            return true;
        }

        public bool DeleteTrack(int id)
        {
            if (id < 0 || id >= _data.Count || _data[id] is null) return false;
            _data[id] = null;
            return true;
        }

        public IEnumerable<Track> FilterByArtist(string match) => _data.Where(t => (t is not null) && t.Artist.StartsWith(match, StringComparison.InvariantCultureIgnoreCase)).Select(t => t!);
    }

    public record Track(string Title, string Artist, string Label, int Year);
}
