using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/事件/[Remove]关闭打印",false,1538437016)]
		static void Do_EventLog_disable_event_log_Remove(){
				BeforeDo_EventLog_disable_event_log_Remove();
				Excute("EventLog","disable_event_log_Remove",_Do_EventLog_disable_event_log_Remove);
				AfterDo_EventLog_disable_event_log_Remove();

		}

		static partial void BeforeDo_EventLog_disable_event_log_Remove();
		static partial void AfterDo_EventLog_disable_event_log_Remove();
		static void _Do_EventLog_disable_event_log_Remove(){
				EditorTools.ChangeSymbol("EVENT_LOG_OFF" , true , true);
		}
		[MenuItem("[Tools]/事件/[Remove]关闭打印", true,1538437016)]
		static bool Do_EventLog_disable_event_log_RemoveCondition(){
				return EditorTools.CheckHasSymbol("EVENT_LOG_OFF");
		}

	}
}
