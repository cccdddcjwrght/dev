using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/事件/[Add]关闭打印",false,1538437016)]
		static void Do_EventLog_disable_event_log_Add(){
				BeforeDo_EventLog_disable_event_log_Add();
				Excute("EventLog","disable_event_log_Add",_Do_EventLog_disable_event_log_Add);
				AfterDo_EventLog_disable_event_log_Add();

		}

		static partial void BeforeDo_EventLog_disable_event_log_Add();
		static partial void AfterDo_EventLog_disable_event_log_Add();
		static void _Do_EventLog_disable_event_log_Add(){
				EditorTools.ChangeSymbol("EVENT_LOG_OFF" , true);
		}
		[MenuItem("[Tools]/事件/[Add]关闭打印", true,1538437016)]
		static bool Do_EventLog_disable_event_log_AddCondition(){
				return !EditorTools.CheckHasSymbol("EVENT_LOG_OFF");
		}

	}
}
