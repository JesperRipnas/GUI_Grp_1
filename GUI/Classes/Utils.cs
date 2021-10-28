using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class GeneralError
    {
        public static readonly string fileAlreadyExist = "File already exists, cannot add it twice";
        public static readonly string confirmationRemoveFilesInList = "Are you sure you want to clear the list?";
        public static readonly string needAtLeastOneFile = "You need to add at least one file before mölking";
        public static readonly string noMolkFilePath = "Filepath doesn't contain .molk";
        
    }
    public class SuccsessMessage
    {
        public static readonly string MolkSuccessMessage = "You successfully packed to a molk archive";
        public static readonly string UnmolkSuccessMessage = "You successfully unpacked the molk archive";
    }
    public class HelpMessage
    {
        public static readonly string Help = "This is help";

    }
    public class Headers
    {
        public static readonly string Molkinator = "The Molkinator System";

    }
}
