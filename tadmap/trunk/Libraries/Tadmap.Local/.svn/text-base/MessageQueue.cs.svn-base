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

      private class Message : IMessage
      {
         public string Content { get; set; }
         public string Path { get; set; }
      }

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
         string messageName = _path + "/" + Guid.NewGuid() + "." + DateTime.Now.Ticks;

         using (FileStream messageFile = File.Open(messageName, FileMode.CreateNew, FileAccess.Write))
         {
            using (StreamWriter writer = new StreamWriter(messageFile))
               writer.Write(message);
         }
      }

      private long TicksFromPath(string path)
      {
         return long.Parse(Path.GetExtension(Path.GetFileName(path)).Substring(1));
      }

      public IMessage Next(int timeout)
      {
         var available = from file in Directory.GetFiles(_path)
                         where TicksFromPath(file) <= DateTime.Now.Ticks
                         select file;

         if (available.Count() == 0)
            return null;

         string message = null;

         string name = available.First();

         using (FileStream messageFile = File.OpenRead(name))
         {
            StreamReader reader = new StreamReader(messageFile);
            message = reader.ReadToEnd();
         }

         string newName = Path.ChangeExtension(name, DateTime.Now.AddMilliseconds(timeout).Ticks.ToString());

         File.Move(name, newName);

         return new Message { Content = message, Path = newName };
      }

      public void Remove(IMessage message)
      {
         if (message is Message)
         {
            Message fileMessage = message as Message;
            File.Delete(fileMessage.Path);
         }
         else
         {
            throw new NotSupportedException("Cannot remove messages of type '" + message.GetType() + "'");
         }
      }

      #endregion
   }
}
