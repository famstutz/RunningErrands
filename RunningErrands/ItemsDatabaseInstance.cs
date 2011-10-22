using System;
using System.Collections.Generic;
using RunningErrands.Model;
using Wintellect.Sterling.Database;

namespace RunningErrands
{
    public class ItemsDatabaseInstance : BaseDatabaseInstance
    {
        public override string Name
        {
            get
            {
                return "ItemsDatabase";
            }
        }

        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition>


                       {
                           // we are only registering the To Do list item with the Sterling engine in this example
                           CreateTableDefinition<ListItem, int>(testModel => testModel.Key)
                       };
        }

        protected string DATAINDEX
        {
            get { return "dataIndex"; }
        }
    }

}