using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NumaLyrics.Players
{
    class iTunes : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly iTunes instance = new iTunes();

        private delegate void GenericEventHandler();

        public delegate void TrackEventHandler(dynamic track);

        public TrackEventHandler OnPlayerPlay = null;
        public TrackEventHandler OnPlayerStop = null;
        public TrackEventHandler OnPlayerTrackChanged = null;

        private dynamic iTunesApp;

        private bool disposed = false;
        private bool playing = false;
        private dynamic currentTrack = null;

        public iTunes()
        {
            Type iTunesType = Type.GetTypeFromProgID("iTunes.Application");
            this.iTunesApp = Activator.CreateInstance(iTunesType);

            this.playing = this.getPlayerState() != 0;
            this.currentTrack = this.getCurrentTrack();

            iTunesApp.OnPlayerPlayEvent += new TrackEventHandler(_OnPlayerPlay);
            iTunesApp.OnPlayerStopEvent += new TrackEventHandler(_OnPlayerStop);
            iTunesApp.OnPlayerPlayingTrackChangedEvent += new TrackEventHandler(_OnPlayerPlayingTrackChanged);
            iTunesApp.OnAboutToPromptUserToQuitEvent += new GenericEventHandler(iTunesQuitEvent);

            this.OnPlayerTrackChanged += _OnPlayerTrackChanged;

#if DEBUG
            Logger.Debug("============== iTunes ==============");
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this.iTunesApp))
            {
                Logger.Debug("{key}: {value}", prop.Name, prop.GetValue(this.iTunesApp));
            }
#endif
        }

        private void _OnPlayerPlay(dynamic track)
        {   
            if (currentTrack == null || currentTrack.TrackDatabaseID != track.TrackDatabaseID)
            {
                this.OnPlayerTrackChanged(track);
            }

            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);

            this.currentTrack = track;
            this.playing = true;
            this.OnPlayerPlay(track);
        }

        private void _OnPlayerStop(dynamic track)
        {
            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);
            this.currentTrack = track;
            this.playing = false;
            this.OnPlayerStop(track);

            if (getCurrentTrack() == null)
            {
                this.currentTrack = null;
                this.OnPlayerTrackChanged(null);
            }
        }

        private void _OnPlayerTrackChanged(dynamic track)
        {
            if (track == null) return;
            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);
        }

        private void _OnPlayerPlayingTrackChanged(dynamic track)
        {
            Logger.Debug("{0}: {1}", MethodBase.GetCurrentMethod().Name, track.Name);
            this.currentTrack = track;
        }

        private void iTunesQuitEvent()
        {
            Logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name);
            this.Dispose();
            System.Windows.Forms.Application.Exit();
        }

        public object getCurrentTrack()
        {
            try
            {
                return iTunesApp.CurrentTrack;
            }
            catch (COMException)
            {
                return null;
            }
        }

        public object getPlayerPosition()
        {
            try
            {
                return iTunesApp.PlayerPosition;
            }
            catch (COMException)
            {
                return null;
            }
        }

        // Undocumented API
        public object getPlayerPositionMS()
        {
            // Force update
            iTunesApp.Resume();

            try
            {
                return iTunesApp.PlayerPositionMS;
            }
            catch (COMException)
            {
                return null;
            }
        }

        public int getPlayerState()
        {
            return iTunesApp.PlayerState;
        }

        public object getVersion()
        {
            return iTunesApp.Version;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                // Release the COM object
                Marshal.ReleaseComObject(this.iTunesApp);
                this.iTunesApp = null;

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

        public dynamic CurrentTrack
        {
            get
            {
                return currentTrack;
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
