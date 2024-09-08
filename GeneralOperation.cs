using System;
using HarmonyLib;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

namespace QuestTeleport
{
	public class GeneralOperation
	{
		[HarmonyPatch(typeof(XUiC_QuestListWindow), nameof(XUiC_QuestListWindow.Init))]
		static class QuestListWindow_Init_Patch
		{
			private static XUiV_Button btn_TeleToQuest;

			static void Postfix(XUiC_QuestListWindow __instance, XUiC_QuestList ___questList)
			{
				btn_TeleToQuest = __instance.GetChildById("btnTeleToQuest").ViewComponent as XUiV_Button;
				btn_TeleToQuest.Controller.OnPress += delegate
				{
					var selectEntry = ___questList.selectedEntry;
					if (selectEntry is null) return;
					if (CheckAvailable(selectEntry.quest, __instance.xui))
						ThreadManager.StartCoroutine(HandleQuestTele(selectEntry.quest));
				};
			}
		}

		sealed class TooltipMono : MonoBehaviour
		{
			private GUIStyle fGUIStyle;
			private float m_interval;

			private void Start()
			{
				fGUIStyle = new GUIStyle();
				fGUIStyle.fontSize = 100;
				fGUIStyle.fontStyle = FontStyle.Bold;
				fGUIStyle.normal.textColor = Color.white;
				fGUIStyle.clipping = TextClipping.Overflow;
				m_interval = Customization.TELEPORT_DELAY;
				InvokeRepeating(nameof(OnChange), 0, 1);
			}

			private void OnChange()
			{
				if (m_interval > 0)
					m_interval -= 1;
				if (m_interval < 4)
					fGUIStyle.normal.textColor = Color.red;
			}
			
			private void OnGUI()
			{
				GUI.Label(new Rect(15f, 15f, 500f, 101f), "传送倒计时：" + m_interval.ToString() + "秒", fGUIStyle);
			}
		}

		private static bool CheckAvailable(Quest _q, XUi _xui)
		{
			if (!_q.HasPosition)
			{
				XUiC_MessageBoxWindowGroup.ShowMessageBox(_xui, "提示", "该任务不具有POI任务点。", default, default, default, false);
				return false;
			}
			if (_q.CurrentState is Quest.QuestState.Completed or Quest.QuestState.Failed)
			{
				XUiC_MessageBoxWindowGroup.ShowMessageBox(_xui, "提示", "该任务状态目前不可用，无法传送至其POI任务点。", default, default, default, false);
				return false;
			}
			if (!_q.Tracked)
			{
				XUiC_MessageBoxWindowGroup.ShowMessageBox(_xui, "提示", "你必须先切换跟踪该任务才可以传送至其POI任务点。", default, default, default, false);
				return false;
			}
			if (_xui.playerUI.entityPlayer.bag.GetItemCount(ItemStack.FromString(Customization.TELEPORT_ITEM_NAME).itemValue) < Customization.TELEPORT_ITEM_COUNT)
			{
				string chznItemId = Localization.Get(Customization.TELEPORT_ITEM_NAME, GamePrefs.GetString(EnumGamePrefs.Language));
				XUiC_MessageBoxWindowGroup.ShowMessageBox(_xui, "提示", $"你所拥有的传送道具\"{chznItemId}\"不足，需要x{Customization.TELEPORT_ITEM_COUNT}", default, default, default, false);
				return false;
			}
			return true;
		}

		private static IEnumerator HandleQuestTele(Quest quest)
		{
			EntityPlayerLocal player = quest.OwnerJournal.OwnerPlayer.PlayerUI.entityPlayer;
			player.SetControllable(false);
			player.PlayerUI.gameObject.SetActive(false);
			if (!player.transform.GetComponent<TooltipMono>())
				player.transform.gameObject.AddComponent<TooltipMono>();
			yield return new WaitForSeconds(Math.Abs(Customization.TELEPORT_DELAY));
			player.SetPosition(quest.NavObject.TrackedPosition);
			player.Respawn(RespawnType.Teleport);
			GameObject.Destroy(player.transform.GetComponent<TooltipMono>());
			yield return new WaitForSeconds(1);
			player.emodel.avatarController.PlayPlayerFPRevive();
			player.bag.DecItem(ItemStack.FromString(Customization.TELEPORT_ITEM_NAME).itemValue,
				Customization.TELEPORT_ITEM_COUNT);
			player.SetControllable(true);
			player.PlayerUI.gameObject.SetActive(true);
			if (Customization.ENABLE_TELE_BROADCAST)
			{
				string content = $"玩家\"{player.EntityName}\"于{DateTime.Now.ToLongTimeString()}使用了POI任务点传送功能。";
				GameManager.Instance.ChatMessageServer(null, EChatType.Global, -1, content, null, EMessageSender.Server);
			}
		}
    }
}

