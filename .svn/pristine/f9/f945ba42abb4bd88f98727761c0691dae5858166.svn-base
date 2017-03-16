using Gmou.DomainModelEntities.StoreBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.BusinessAccessLayer
{
   public class BALBookStore
    {

       public static bool BALInsertChitBook(ChitBookModel model)
       {

           return DataRepository.BookStoreRepository.InsertChitBook(model);
       }

       public static List<ChitBookDetails> BALGetAllChitBookDetails()
       {

           return DataRepository.BookStoreRepository.GetAllChitBookDetails().ToList();
       }
    }
}
