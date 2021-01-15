using System;
using System.Collections.Generic;
using System.Text;
using Priority_Queue;
using Google.Apis.Gmail.v1.Data;
using System.Diagnostics.CodeAnalysis;
using Cleanbox.DatabaseAccess;

namespace Cleanbox
{

    public class InboxSorter
    {
        public static List<MessagePrioritiesModel> SortInbox(List<Message> inbox)
        {
            List<string> senders = GetSenderNames(inbox);
            SimplePriorityQueue<string> queue = PopulatePriorityQueue(senders);
            List<MessagePrioritiesModel> sortedSenders = PriorityQueueToList(queue);
            return sortedSenders;
        }

        private static List<string> GetSenderNames(List<Message> inbox)
        {
            List<string> senders = new List<string>();
            foreach(Message message in inbox)
            {
                string sender = GetSenderName(message);
                senders.Add(sender);
            }
            return senders;
        }

        private static string GetSenderName(Message message)
        {
            foreach(MessagePartHeader header in message.Payload.Headers)
            {
                if (header.Equals("From"))
                {
                    return header.Value;
                }
            }
            return "";
        }

        private static SimplePriorityQueue<string> PopulatePriorityQueue(List<string> senders)
        {
            SimplePriorityQueue<string> queue = new SimplePriorityQueue<string>();

            //If sender exists in queue, increment priority, else add to queue with value 1
            foreach (string sender in senders)
            {
                if (queue.Contains(sender))
                {
                    float priority = queue.GetPriority(sender);
                    queue.UpdatePriority(sender, priority++);
                }
                else
                {
                    queue.Enqueue(sender, 1);
                }
            }

            return queue;
        }

        private static List<MessagePrioritiesModel> PriorityQueueToList(SimplePriorityQueue<string> senderQueue)
        {
            List<MessagePrioritiesModel> senderPriorities = new List<MessagePrioritiesModel>();
            while (senderQueue.Count != 0)
            {
                string first = senderQueue.First;
                float priority = senderQueue.GetPriority(first);
                MessagePrioritiesModel newMessagePriorities = new MessagePrioritiesModel()
                {
                    categoryName = first,
                    categoryPriority = priority
                };
                senderPriorities.Add(newMessagePriorities);

                senderQueue.Dequeue();
            }

            return senderPriorities;
        }

    }
}
