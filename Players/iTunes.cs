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
            Type iTunesType = Type.GetTypeFromProgID("iTunes.Application");
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
            Logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name);
            this.Dispose();
            System.Windows.Forms.Application.Exit();
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
