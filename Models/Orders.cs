using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderXML.Models
{
    public class Orders
    {
        private string fileName = @"c:\temp\test.xml";
        public DataSet LoadXML()
        {
            DataSet dataSet = new DataSet();
            try
            {
                dataSet.ReadXml(fileName, XmlReadMode.InferSchema);
                DataTable tableSource = dataSet.Tables["Order"];
                DataTable tableValid = tableSource.Clone();
                tableValid.TableName = "ValidOrder";
                foreach (DataRow row in tableSource.Rows)
                {
                    if (OrderValid.IsValid(row))
                        tableValid.Rows.Add(row.ItemArray);
                }

                dataSet.Tables.Add(tableValid);
            }
            catch (Exception e)
            {
                DataTable table = new DataTable("ValidOrder");
                dataSet.Tables.Add(table);
            }
            return dataSet;
        }
    }

}
