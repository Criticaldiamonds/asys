using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsysEditor.Classes
{
    /// <summary>
    /// Represents an Unique Control Identifier.</summary>
    /// <remarks>
    /// The UCID is used to help distinguish between controls.
    /// </remarks>
    class UCID
    {
        private static List<int> _existingUCIDs = new List<int>();

        /// <summary>
        /// Generate a new UCID</summary>
        /// <returns>A UCID for use by a control</returns>
        public static int GenerateUCID()
        {
            int ucid = new Random().Next(0, 9999);

            // Prevent duplicates
            while (_existingUCIDs.Contains(ucid))
            {
                ucid = new Random().Next(0, 9999);
            }

            _existingUCIDs.Add(ucid);
            return ucid;
        }

        /// <summary>
        /// Removes an ID
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveUCID(int id)
        {
            _existingUCIDs.Remove(id);
        }
    }
}
