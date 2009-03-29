using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Messaging;
using System.IO;

namespace Tadmap.Local
{
   public class MessageQueue : IMessageQueue
   {
      string _path;

      private MessageQueue()
      {
         throw new NotSupportedException("You must use constructor that gives a folder name.");
      }

      public MessageQueue(string folderPath)
      {
         _path = folderPath;
      }

      #region IMessageQueue Members
    
      public void Add(string message)
      {
         string messageName = _path + "/" + Guid.NewGuid() + ".new";

         using (FileStream messageFile = File.Open(messageName, FileMode.CreateNew, FileAccess.Write))
         {
            using (StreamWriter writer = new StreamWriter(messageFile))
               writer.Write(message);
         }
      }

      public string Next()
      {
         string messageName = Directory.GetFiles(_path, "*.new").FirstOrDefault();
         string message = null;

         if (messageName != null)
         {
            using (FileStream messageFile = File.OpenRead(messageName))
            {
               StreamReader reader = new StreamReader(messageFile);
               message = reader.ReadLine();
            }

            File.Move(messageName, Path.ChangeExtension(messageName, "old"));
         }

         return message;
      }

      public void ReviveMessages()
      {
         foreach (string messageName in Directory.GetFiles(_path, "*.old"))
         {
            File.Move(messageName, Path.ChangeExtension(messageName, "new"));
         }
      }

      #endregion
   }
}
