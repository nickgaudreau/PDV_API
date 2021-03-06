﻿namespace Services
{
    public interface ITokenAuthServices
    {
        #region Interface member methods.

        /// <summary>
        /// Checks if a token is valid.
        /// </summary>
        /// <param name="token">string - generated either by GenerateToken() or via client with cryptojs etc.</param>
        /// <param name="ip">string - IP address of client, passed in by RESTAuthenticate attribute on controller.</param>
        /// <param name="userAgent">string - user-agent of client, passed in by RESTAuthenticate attribute on controller.</param>
        /// <returns>bool</returns>
        bool IsTokenValid(string token, string ip, string userAgent);

        #endregion
    }
}
