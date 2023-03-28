using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Ejercicio2_2.Models;
using System.IO;

namespace Ejercicio2_2.Controllers
{
    public class BDFirma
    {
        readonly SQLiteAsyncConnection BD;

        public BDFirma(string pathdb)
        {
            BD = new SQLiteAsyncConnection(pathdb);
            BD.CreateTableAsync<ModelFirma>().Wait();
        }

        public Task<List<ModelFirma>> GetFirmasList()
        {
            return BD.Table<ModelFirma>().ToListAsync();
        }

        public Task<ModelFirma> GetFirmaID(int pcodigo)
        {
            return BD.Table<ModelFirma>()
                .Where(i => i.Id == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GuadarFirma(ModelFirma firma)
        {
            if (firma.Id != 0)
            {
                return BD.UpdateAsync(firma);
            }
            else
            {
                return BD.InsertAsync(firma);
            }
        }

        public Task<int> EliminarFirma(ModelFirma firma)
        {
            return BD.DeleteAsync(firma);
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
