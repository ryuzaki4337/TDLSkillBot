// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.14.0

using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;
using OfferRedeemBot.API;

namespace OfferRedeemBot.Dialogs
{
    public class OrderRedeemDialog : CancelAndHelpDialog
    {
        private const string DestinationStepMsgText = "Where would you like to travel to?";
        private const string OriginStepMsgText = "Where are you traveling from?";

        public OrderRedeemDialog()
            : base(nameof(OrderRedeemDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new DateResolverDialog());
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                ListOffersStepAsync,
                OnlineOrOfflineStepAsync,
                ProductCollectionStepAsync,
                APIFetchStepAsync,
                FinalMessageStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }
        
        private static async Task<DialogTurnResult> ListOffersStepAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            var offerNotAbleToMessage =
                "Sure, In order to help you out, I’ll need to confirm a few things with you. Please select the offer that you are unable to redeem from the below mentioned list";

            //var offerList = GetAllOffersAPI.GetOffersCustomerId(customerId);

            var offerList = new List<string> {"CROMA50", "1MG10"};
 
            return await stepcontext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text(offerNotAbleToMessage),
                    Choices = ChoiceFactory.ToChoices(offerList),
                }, cancellationtoken);
        }


        private static async Task<DialogTurnResult> OnlineOrOfflineStepAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            // Store the object
            stepcontext.Values["offerSelected"] = ((FoundChoice)stepcontext.Result).Value;

            var orderedPlaceMessage = "Please let me know where are you trying to redeem this offer";
            
            return await stepcontext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text(orderedPlaceMessage),
                    Choices = ChoiceFactory.ToChoices(new List<string> {"Online", "Offline At Store"}),
                }, cancellationtoken);

        }

        private static async Task<DialogTurnResult> ProductCollectionStepAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            stepcontext.Values["placeOrdered"] = ((FoundChoice)stepcontext.Result).Value;

            var tempMessage = $"Offer Selected: {stepcontext.Values["offerSelected"]}, and the place you ordered is {stepcontext.Values["placeOrdered"]}";

            await stepcontext.Context.SendActivityAsync(MessageFactory.Text(tempMessage), cancellationtoken);

            return await stepcontext.NextAsync(null, cancellationtoken);

            // Here we'll get the offer details

            // Bot compares whether the offer can be used offline/online and then it'll check whether the offer is useful in that city.

            // If the checking function returns true, then proceed to the next step, else end the dialog here with the error messages.
        }

        private Task<DialogTurnResult> APIFetchStepAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            // Using the product and the offer selected, the bot compares IDs and throws an error or a sorry message.
            throw new System.NotImplementedException();
        }


        private Task<DialogTurnResult> FinalMessageStepAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            throw new System.NotImplementedException();
        }
    }
}
