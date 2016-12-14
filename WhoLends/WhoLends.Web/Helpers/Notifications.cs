using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhoLends.Web.Helpers
{
    public static class Notifications
    {
        private const string ModelMessageKey = "modelMessages";

        /// <summary>
        /// Adds an error message to the TempData of the controller.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <param name="messageText">Message text.</param>
        public static void AddErrorMessage(this Controller controller, string messageText)
        {
            AddModelMessage(controller, messageText, ModelMessage.MessageType.Error);
        }

        /// <summary>
        /// Adds a info message to the TempData of the controller.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <param name="messageText">Message text.</param>
        public static void AddInfoMessage(this Controller controller, string messageText)
        {
            AddModelMessage(controller, messageText, ModelMessage.MessageType.Info);
        }

        /// <summary>
        /// Adds a success message to the TempData of the controller.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <param name="messageText">Message text.</param>
        public static void AddSuccessMessage(this Controller controller, string messageText)
        {
            AddModelMessage(controller, messageText, ModelMessage.MessageType.Success);
        }

        private static void AddModelMessage(this Controller controller, string messageText, ModelMessage.MessageType messageType)
        {
            List<ModelMessage> modelMessages;
            if (controller.TempData[ModelMessageKey] == null)
            {
                modelMessages = new List<ModelMessage>();
            }
            else
            {
                modelMessages = controller.TempData[ModelMessageKey] as List<ModelMessage>;
            }

            if (modelMessages != null)
            {
                modelMessages.Add(new ModelMessage
                {
                    MessageTest = messageText,
                    Type = messageType
                });

                controller.TempData[ModelMessageKey] = modelMessages;
            }
        }
    }

    /// <summary>
    /// Class that encapsulates a model message. Used on views when we want to display sme custom message sent from the controller.
    /// </summary>
    public class ModelMessage
    {
        public enum MessageType
        {
            Error,
            Info,
            Success
        }

        public MessageType Type { get; set; }

        public string MessageTest { get; set; }
    }
}