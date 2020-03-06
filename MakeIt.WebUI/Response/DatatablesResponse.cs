using System.Collections.Generic;

namespace MakeIt.WebUI.Response
{
    public class DataTablesResponse<T> : IDatatablesResponse<T>
    {
        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public IEnumerable<T> data { get; set; }

        public string error { get; set; }

        public DataTablesResponse() { }

        public DataTablesResponse(int draw, int recordsTotal, int recordsFiltered, IEnumerable<T> data)
        {
            this.draw = draw;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
            this.data = data;
        }

        public DataTablesResponse(string error)
        {
            this.error = error;
        }
    }
}