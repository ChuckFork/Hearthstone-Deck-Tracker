#region

using System.Linq;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;

#endregion

namespace Hearthstone_Deck_Tracker
{
	public class Secret
	{
		public Secret(string cardId, int count)
		{
			CardId = cardId;
			Count = count;
		}

		public string CardId { get; }
		public int Count { get; set; }

		public bool ActiveDeckIsConstructed => DeckList.Instance.ActiveDeck != null && !DeckList.Instance.ActiveDeck.IsArenaDeck;


		public int AdjustedCount(GameV2 game)
		{
			return (Config.Instance.AutoGrayoutSecrets
			        && (game.CurrentGameMode == GameMode.Casual || game.CurrentGameMode == GameMode.Ranked
			            || game.CurrentGameMode == GameMode.Friendly || game.CurrentGameMode == GameMode.Practice || ActiveDeckIsConstructed)
			        && game.Opponent.RevealedCards.Where(x => x != null && x.Entity != null)
			               .Count(x => x.Entity.Id < 68 && x.Entity.CardId == CardId) >= 2) ? 0 : Count;
		}
	}
}