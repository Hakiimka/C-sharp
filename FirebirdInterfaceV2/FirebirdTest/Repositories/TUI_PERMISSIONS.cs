using System;

namespace FirebirdTest.Repositories
{
    public class TUI_PERMISSIONS
    {
        public virtual int Id { get; set; }

        public virtual int View_Model_Action { get; set; }
        public virtual int User_Id { get; set; }

        public virtual DateTime Date_Expire { get; set; }

    }
}
