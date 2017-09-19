using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWorkDB.V1;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Windows;

namespace AgileFileManagerv1.FloatWindows.FW_Client.Model
{
    public class VW_Clients
    {
        public List<Client> clients;
        private DataTable dt;

        public VW_Clients()
        {
            dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Código", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Distribuidor", typeof(string));
        }

        public void UpdateTable(AgileManagerDB db)
        {
            clients = db.Clients.Include(c => c.dealer).ToList();

            dt.Clear();
            foreach (Client item in clients)
            {
                if(item.dealer != null)
                    dt.Rows.Add(item.ClientID, item.Code, item.Name, item.dealer.Name);
                else
                    dt.Rows.Add(item.ClientID, item.Code, item.Name, "");
            }
        }

        public IEnumerable GetTable(AgileManagerDB db)
        {
            UpdateTable(db);
            return dt.DefaultView;
        }
    }
}
