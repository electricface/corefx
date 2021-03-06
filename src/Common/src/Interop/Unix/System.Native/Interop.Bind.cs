// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Sys
    {
        [DllImport(Libraries.SystemNative, EntryPoint = "SystemNative_Bind")]
        private static extern unsafe Error DangerousBind(int socket, byte* socketAddress, int socketAddressLen);

        internal static unsafe Error Bind(SafeHandle socket, byte* socketAddress, int socketAddressLen)
        {
            bool release = false;
            try
            {
                socket.DangerousAddRef(ref release);
                return DangerousBind((int)socket.DangerousGetHandle(), socketAddress, socketAddressLen);
            }
            finally
            {
                if (release)
                {
                    socket.DangerousRelease();
                }
            }
        }
    }
}
