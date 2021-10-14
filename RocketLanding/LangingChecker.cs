using System;
using System.Collections.Generic;
using System.Linq;

namespace RocketLanding
{
    /// <summary>
    /// Class that will be used to by rocket to check.
    /// </summary>
    public class LangingChecker
    {
        /// <summary>
        /// Defualt Constracter
        /// </summary>
        public LangingChecker()
        {
            UpdateSettings(new LandingAreaSettings()
            {
                LandingAreaSize = 100,
                LandingPlatformSize = 10,
                LandingPlatformStartX = 5,
                LandingPlatformStartY = 5
            });
            rocketsLastChecks = new Dictionary<int, List<int>>();
        }
        /// <summary>
        /// Counstracter for new Landing checker with custom Landing settings 
        /// </summary>
        /// <param name="settings">Landing Area Settings</param>
        public LangingChecker(LandingAreaSettings settings)
        {
            UpdateSettings(settings);
            rocketsLastChecks = new Dictionary<int, List<int>>();
        }
        //list to save Rockets last check cordnations
        private Dictionary<int, List<int>> rocketsLastChecks { get; set; }

        //Local settings 
        private LandingAreaSettings settings { get; set; }
        /// <summary>
        /// This method checks if landing location status.
        /// </summary>
        /// <param name="x">Landing x cordnations </param>
        /// <param name="y">Landing y cordnations</param>
        /// <returns>values: 'out of platform', 'clash', 'ok for landing'</returns>
        public string Check(int id, int x, int y)
        {
            //Chek if out of landing platform
            if (IsInPlatform(x, y))
            {
                //Check if it is in clash area - other rockets last check 
                foreach (var rocket in rocketsLastChecks.Where(x => x.Key != id))
                {
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (x == rocket.Value[0] + i && y == rocket.Value[1] + j)
                            {
                                return "clash";
                            }
                        }
                    }
                }
                //Remove this rocket Last Check if exist
                rocketsLastChecks.Remove(id);
                //Add new succeeded check for this rocket
                rocketsLastChecks.Add(id, new List<int> { x, y });
                return "ok for landing";
            }
            else
            {
                return "out of platform";
            }
        }
        /// <summary>
        /// Method to check if point in the platform or not
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Return True if the point in the platform</returns>
        private bool IsInPlatform(int x, int y)
        {
            return ((settings.LandingPlatformStartX <= x && x < settings.LandingPlatformStartX + settings.LandingPlatformSize) && (settings.LandingPlatformStartY <= y && y < settings.LandingPlatformStartY + settings.LandingPlatformSize));
        }
        /// <summary>
        /// Method to check if point in the Landing Area or not
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Return True if the point in the Landing Area</returns>
        private bool IsInLandingArea(int x, int y)
        {
            //pint in landing area
            return ((0 <= x && x <= settings.LandingAreaSize) && (0 <= y && y <= settings.LandingAreaSize));
        }
        /// <summary>
        /// This method get the settings from Congig file and update local settings Variables. 
        /// </summary>
        public void UpdateSettings(LandingAreaSettings settings)
        {
            this.settings = settings;
            //checking if platform out of landing area. 
            if (!(IsInLandingArea(settings.LandingPlatformStartX, settings.LandingPlatformStartY) && IsInLandingArea(settings.LandingPlatformStartX + settings.LandingPlatformSize - 1, settings.LandingPlatformStartY + settings.LandingPlatformSize - 1)))
            {
                throw new Exception("Landing Platform is out of landing area.");
            }
        }
    }
}
