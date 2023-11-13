using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.TehPrelucrareDb
{
        using System.Data;
        using System.Data.SqlClient;

        public class AdventureWorksDataAccess
        {
            private DataTable _dataTable;
            private SqlDataAdapter _adapter;
            private SqlConnection _connection;

            private int _currentIndex = 0;
            public int CurrentIndex => _currentIndex;
            private SqlCommandBuilder _commandBuilder;

            public DataTable DataTable
            {
                get { return _dataTable; }
            }

            public AdventureWorksDataAccess(string tableName)
            {
                _connection = new SqlConnection("Server=localhost;Database=AdventureWorksDW2022;Trusted_Connection=True;");
                _connection.Open();
                var query = $"SELECT * FROM {tableName}";
                _adapter = new SqlDataAdapter(query, _connection);
                _dataTable = new DataTable();
                _commandBuilder = new SqlCommandBuilder(_adapter);

                _adapter.Fill(_dataTable);
            }


            public (bool hasNext, DataRow data) ReadCurrent()
            {
                var hasNext = _currentIndex < _dataTable.Rows.Count - 1;
                var data = _dataTable.Rows.Count > 0 ? _dataTable.Rows[_currentIndex] : null;
                return (hasNext, data);
            }

            public (bool hasNext, DataRow data) ReadNext()
            {
                if (_currentIndex < _dataTable.Rows.Count - 1)
                {
                    _currentIndex++;
                }
                return ReadCurrent();
            }

            public (bool hasPrevious, DataRow data) ReadPrevious()
            {
                var hasPrevious = _currentIndex > 0;
                if (hasPrevious)
                {
                    _currentIndex--;
                }
                hasPrevious = _currentIndex > 0;

                return (hasPrevious, _dataTable.Rows.Count > 0 ? _dataTable.Rows[_currentIndex] : null);
            }


            public void UpdateCurrent(DataRow updatedRow)
            {
                if (_currentIndex >= 0 && _currentIndex < _dataTable.Rows.Count)
                {
                    var currentRow = _dataTable.Rows[_currentIndex];
                    foreach (DataColumn column in _dataTable.Columns)
                    {
                        currentRow.BeginEdit();
                        currentRow[column.ColumnName] = updatedRow[column.ColumnName];
                        currentRow.EndEdit();
                    }
                }

            }


            public void DeleteCurrent()
            {
                if (_currentIndex >= 0 && _currentIndex < _dataTable.Rows.Count)
                {
                    _dataTable.Rows[_currentIndex].Delete();
                    CommitChanges();

                    if (_currentIndex == _dataTable.Rows.Count)
                    {
                        _currentIndex--;
                    }
                }
            }



            public void ResetIndexToLastValid()
            {
                if (_dataTable.Rows.Count > 0)
                {
                    _currentIndex = _dataTable.Rows.Count - 1;
                }
                else
                {
                    _currentIndex = -1;
                }
            }


            public static string[] GetTableNames()
            {
                List<string> tableNames = new List<string>();
                using (var connection = new SqlConnection("Server=localhost;Database=AdventureWorksDW2022;Trusted_Connection=True;"))
                {
                    connection.Open();
                    DataTable schema = connection.GetSchema("Tables");
                    foreach (DataRow row in schema.Rows)
                    {
                        if (row["TABLE_TYPE"].ToString() == "BASE TABLE")
                        {
                            tableNames.Add(row["TABLE_NAME"].ToString());
                        }
                    }
                }
                return tableNames.ToArray();
            }

            public DataRow GetFirstRecord(string tableName)
            {
                ReloadData(tableName);
                _currentIndex = 0;
                return _dataTable.Rows.Count > 0 ? _dataTable.Rows[0] : null;
            }

            public void InsertRecord(DataRow newRow)
            {
                if (_dataTable.Columns.Count != newRow.ItemArray.Length)
                {
                    throw new ArgumentException("The number of values must match the number of columns in the table.");
                }

                _dataTable.Rows.Add(newRow);
            }

            public void CommitChanges()
            {
                if (_connection == null || _connection.State != ConnectionState.Open)
                {
                    throw new InvalidOperationException("The database connection is not open.");
                }

                _commandBuilder = new SqlCommandBuilder(_adapter);

                _adapter.InsertCommand = _commandBuilder.GetInsertCommand();
                _adapter.UpdateCommand = _commandBuilder.GetUpdateCommand();
                _adapter.DeleteCommand = _commandBuilder.GetDeleteCommand();

                _adapter.InsertCommand.Connection = _connection;
                _adapter.UpdateCommand.Connection = _connection;
                _adapter.DeleteCommand.Connection = _connection;

                _adapter.Update(_dataTable);

                _dataTable.AcceptChanges();
            }




            public void ReloadData(string tableName)
            {
                using (var connection = new SqlConnection("Server=localhost;Database=AdventureWorksDW2022;Trusted_Connection=True;"))
                {
                    var query = $"SELECT * FROM {tableName}";
                    var adapter = new SqlDataAdapter(query, connection);
                    var newDataTable = new DataTable();
                    adapter.Fill(newDataTable);
                    _dataTable = newDataTable;
                    _currentIndex = 0;
                }
            }
        }

}
