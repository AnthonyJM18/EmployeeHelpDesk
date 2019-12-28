/* Program Name : UpdateStatus
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Status Enumeration
 */


using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskDAL
{
    // Enumeration that is used for defining the state of data
    public enum UpdateStatus
    {
        Ok = 1,
        Failed = -1,
        Stale = -2
    };

}
