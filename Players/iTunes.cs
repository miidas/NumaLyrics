using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NumaLyrics.Players
{
    class iTunes : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly iTunes instance = new iTunes();

        private delegate void GenericEventHandler();

        public delegate void TrackEventHandler(object trackObj);

        public TrackEventHandler OnPlayerPlay = null;
        public TrackEventHandler OnPlayerStop = null;
        public TrackEventHandler OnPlayerTrackChanged = null;

        private object itunesCOMObj;

        private bool disposed = false;
        private bool playing = false;
        private object currentTrackObj = null;

        public iTunes()
        {
            // iTunesApp: dc0c2640-1415-4644-875c-6f4d769839ba
            // Invoke-Command -ScriptBlock { 
            //   $iTunes = New - Object - ComObject iTunes.application
            //    $itunes | gm
            // }
            Type iTunesType = Type.GetTypeFromProgID("iTunes.Application");
            //Logger.Debug("GUID: {id}", iTunesType.GUID);
            this.itunesCOMObj = Activator.CreateInstance(iTunesType);

            this.playing = getPlayerState() != 0;
            this.currentTrackObj = getCurrentTrack();

            dynamic itunesCOM = itunesCOMObj;

            itunesCOM.OnPlayerPlayEvent += new TrackEventHandler(_OnPlayerPlay);
            itunesCOM.OnPlayerStopEvent += new TrackEventHandler(_OnPlayerStop);
            itunesCOM.OnPlayerPlayingTrackChangedEvent += new TrackEventHandler(_OnPlayerPlayingTrackChanged);
            itunesCOM.OnAboutToPromptUserToQuitEvent += new GenericEventHandler(iTunesQuitEvent);

            this.OnPlayerTrackChanged += _OnPlayerTrackChanged;

#if DEBUG

            Logger.Debug("============== iTunes ==============");
            
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(itunesCOM))
            {
                Logger.Debug("{key}: {value}", prop.Name, prop.GetValue(itunesCOM));
            }

            /*Logger.Debug("============== CurrentPlaylist ==============");
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(itunesCOM.CurrentPlaylist))
            {
                Logger.Debug("{key}: {value}", prop.Name, prop.GetValue(itunesCOM.CurrentPlaylist));
            }*/

            //Logger.Debug("============== Test ==============");
            /*foreach (var prop in itunesCOM)
            {
                Logger.Debug("{key}", prop.Name);
            }*/
#endif
        }

        private void _OnPlayerPlay(object trackObj)
        {
            dynamic track = trackObj;
            dynamic currentTrack = currentTrackObj;

            if (currentTrack == null || currentTrack.TrackDatabaseID != track.TrackDatabaseID)
            {
                this.OnPlayerTrackChanged(track);
            }

            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);

            this.currentTrackObj = trackObj;
            this.playing = true;
            this.OnPlayerPlay(track);
        }

        private void _OnPlayerStop(object trackObj)
        {
            dynamic track = trackObj;

            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);

            this.currentTrackObj = trackObj;
            this.playing = false;
            this.OnPlayerStop(track);

            if (getCurrentTrack() == null)
            {
                this.currentTrackObj = null;
                this.OnPlayerTrackChanged(null);
            }
        }

        private void _OnPlayerTrackChanged(object trackObj)
        {
            dynamic track = trackObj;

            if (track == null) return;

            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);
        }

        private void _OnPlayerPlayingTrackChanged(object trackObj)
        {
            dynamic track = trackObj;

            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);

            this.currentTrackObj = trackObj;
        }

        private void iTunesQuitEvent()
        {
            Logger.Debug(MethodBase.GetCurrentMethod().Name);
            this.Dispose();
            System.Windows.Forms.Application.Exit();
        }

        public bool TogglePlay()
        {
            dynamic itunesCOM = itunesCOMObj;
            var state = itunesCOM.PlayerState;
            if (state == 1) // ITPlayerStatePlaying
            {
                itunesCOM.Pause();
            }
            else if (state == 0) // ITPlayerStateStopped 
            {
                itunesCOM.Play();
            }
            return state == 0;
        }

        public void Play()
        {
            dynamic itunesCOM = itunesCOMObj;
            itunesCOM.Play();
        }

        public void Stop()
        {
            dynamic itunesCOM = itunesCOMObj;
            itunesCOM.Stop();
        }

        public void Pause()
        {
            dynamic itunesCOM = itunesCOMObj;
            itunesCOM.Pause();
        }

        public int PlayerState
        {
            get
            {
                dynamic itunesCOM = itunesCOMObj;
                return itunesCOM.PlayerState;
            }
        }

        public void NextTrack()
        {
            dynamic itunesCOM = itunesCOMObj;
            itunesCOM.NextTrack();
        }

        public bool ToggleShuffle()
        {
            dynamic itunesCOM = itunesCOMObj;
            var playlist = itunesCOM.CurrentPlaylist;
            if (playlist == null) return false;
            return playlist.Shuffle = !playlist.Shuffle;
        }

        public void Shuffle(bool shouldShuffle)
        {
            dynamic itunesCOM = itunesCOMObj;
            var playlist = itunesCOM.CurrentPlaylist;
            playlist.Shuffle = shouldShuffle;
        }

        public void PlayPlaylistByName(string name)
        {
            dynamic itunesCOM = itunesCOMObj;
            var playlist = itunesCOM.LibrarySource.Playlists.ItemByName(name);
            playlist.PlayFirstTrack();
        }

        public void PlayFile(string path)
        {
            dynamic itunesCOM = itunesCOMObj;
            itunesCOM.PlayFile(path);
        }

        public void Test2()
        {
            dynamic itunesCOM = itunesCOMObj;
            /*foreach (var t in itunesCOM.CurrentPlaylist.Tracks)
            {
                Logger.Debug("Track: {t}", t);
            }*/

            // var t = itunesCOM.GetITObjectByID(72, 15501, 22514, 22508);
            // var t = itunesCOM.GetITObjectByID(20724, 25126, 25675, 25671); // This method can only retrive songs in the main library
            //Logger.Debug("Track: {t}", t);
            //t.Play();

            /*var t = itunesCOM.CurrentTrack;
            Logger.Debug("ITObjectPersistentIDHigh: {t}", itunesCOM.ITObjectPersistentIDHigh(t));
            Logger.Debug("ITObjectPersistentIDLow: {t}", itunesCOM.ITObjectPersistentIDLow(t));*/

            // MIDL_INTERFACE
            // 755D76F1-6B85-4ce4-8F5F-F88D9743DCD8
            //dynamic obj = Marshal.BindToMoniker("new:9DD6680B-3EDC-40DB-A771-E6FE4832E34A");
            // dc0c2640-1415-4644-875c-6f4d769839ba

            // playlist play
            //var playlist = itunesCOM.LibrarySource.Playlists.ItemByName("Internet Songs");
            //playlist.PlayFirstTrack();

            // Playlist -> Source
            var t = itunesCOM.CurrentTrack;
            Logger.Debug("ITObjectPersistentIDHigh: {t}", itunesCOM.ITObjectPersistentIDHigh(itunesCOM.CurrentPlaylist.Source));
            Logger.Debug("ITObjectPersistentIDLow: {t}", itunesCOM.ITObjectPersistentIDLow(itunesCOM.CurrentPlaylist.Source));

            var s = itunesCOM.Sources.ItemByPersistentID(1327072063L, -321238749L);
            if (s != null)
            {
                Logger.Debug("OK");
            }

            Logger.Debug("{s}", itunesCOM.CurrentStreamTitle);
            //var s = itunesCOM.Sources;
            //Logger.Debug("Test: {s}", s[0].Playlists.Item(5).Name);

            // 146420402
            // -1814042804
            //Logger.Debug("Source: {s}", t.Playlist.Source.Kind);// source = 0 = apple music?
            //Logger.Debug("Playlist: {s}", t.Playlist.Kind); //ITPlaylistKindUser */
            //t.Playlist.Tracks.ItemByPersistentID(-781488475, 1516463154).Play();

            //itunesCOM.CurrentPlaylist.Tracks.ItemByPersistentID(146420402, -1814042804).Play();

            /*var s = itunesCOM.Sources;
            foreach (var ss in s)
            {

                Logger.Debug("============== Source ==============");
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(ss))
                {
                    Logger.Debug("{key}: {value}", prop.Name, prop.GetValue(ss));
                }

                foreach (var pl in ss.Playlists)
                {
                    Logger.Debug("============== Playlist ==============");
                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(pl))
                    {
                        Logger.Debug("{key}: {value}", prop.Name, prop.GetValue(pl));
                    }
                    var st = pl.Tracks.ItemByPersistentID(1494575363, 1548077231);
                    if (st != null)
                    {
                        st.Play();
                    }
                }
            }
            Logger.Debug("============== FINISHED! ==============");*/


            //var t = itunesCOM.Sources;
            //foreach(var s in t)
            //{
            //Logger.Debug("Source: {s}", s.Kind); // ITSourceKindUnknown = 0,ITSourceKindLibrary = 1,ITSourceKindRadioTuner = 6
            //s.Playlists.Item(1).ItemByPersistentID(1017571607, -426259576);

            /*if (s.Kind == 1)
            {
                s.Playlists.Item(1).PlayFirstTrack();
            }*/

            //var x = s.Playlists.Count;
            //s.Playlists.Item(1).PlayFirstTrack();
            //if (x != null) Logger.Debug("Source: {s}", s.Playlists.Item(1).PlayFirstTrack());
            //Thread.Sleep(5000);
            //}

            //playlist.Tracks(1).Play();
            /*for (var i = 1; i <= playlist.Tracks.Count; i++)
            {
                var track = playlist.Tracks(i);
                var name = track.Name;
                var artist = track.Artist;
                var filepath = track.Location;
                Logger.Debug(name + "/" + artist + "/" + filepath);
            }*/

            //var t = Type.GetTypeFromCLSID(new Guid("4CB0915D-1E54-4727-BAF3-CE6CC9A225A1"));
            //dynamic obj = Activator.CreateInstance(t);

            //var t = IITTrackCollection.ItemByPersistentID(1017571607, -426259576);
            //Logger.Debug("Object: {t}", t);
            //itunesCOM.C = t;
            //this.Play();
            //t.Play();
        }

        public object getCurrentTrack()
        {
            dynamic itunesCOM = itunesCOMObj;
            try
            {
                return itunesCOM.CurrentTrack;
            }
            catch (COMException)
            {
                return null;
            }
        }

        public object getPlayerPosition()
        {
            dynamic itunesCOM = itunesCOMObj;
            try
            {
                return itunesCOM.PlayerPosition;
            }
            catch (COMException)
            {
                return null;
            }
        }

        // Undocumented API
        public object getPlayerPositionMS()
        {
            dynamic itunesCOM = itunesCOMObj;

            // Force update
            itunesCOM.Resume();

            try
            {
                return itunesCOM.PlayerPositionMS;
            }
            catch (COMException)
            {
                return null;
            }
        }

        public int getPlayerState()
        {
            dynamic itunesCOM = itunesCOMObj;
            return itunesCOM.PlayerState;
        }

        public object getVersion()
        {
            dynamic itunesCOM = itunesCOMObj;
            return itunesCOM.Version;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                // Release the COM object
                Marshal.ReleaseComObject(this.itunesCOMObj);
                this.itunesCOMObj = null;

                disposed = true;
            }
        }

        public bool IsPlaying
        {
            get
            {
                return playing;
            }
        }

        public object CurrentTrackObj
        {
            get
            {
                return currentTrackObj;
            }
        }

        public bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        public static iTunes GetInstance()
        {
            return instance;
        }
    }
}
