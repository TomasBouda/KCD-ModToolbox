cheat_eval cheat:logInfo("X:%s Y:%s Z:%s", tostring(player:GetWorldDir().x), tostring(player:GetWorldDir().y), tostring(player:GetWorldDir().z))

cheat_eval cheat:logInfo("X:%s Y:%s Z:%s", tostring(player:GetWorldPos().x), tostring(player:GetWorldPos().y), tostring(player:GetWorldPos().z))

cheat_eval cheat:logInfo("%s", tostring(player:GetWorldAngles().z))

cheat_eval cheat:logDebug("%s", tostring(player:GetWorldAngles().z))

cheat_eval cheat:logSetLevel(3)

cheat_eval cheat:logInfo(tostring(cheat:get_npc_direct_spawn_point(player:GetWorldPos().x, player:GetWorldPos().y, player:GetWorldPos().z, player:GetWorldDir().x, player:GetWorldDir().y).z))
cheat_eval cheat:cheat_spawn_entity(nil, "NPC", "43590e12-70a9-3aa8-dc56-8dd71a7ce4a7", nil, nil)

cheat_eval cheat:cheat_spawn_entity(nil, "NPC", "43590e12-70a9-3aa8-dc56-8dd71a7ce4a7", cheat:get_npc_direct_spawn_point(player:GetWorldPos().x, player:GetWorldPos().y, player:GetWorldPos().z, player:GetWorldDir().x, player:GetWorldDir().y), player:GetWorldDir())

cheat_spawn_npc token:43590e12-70a9-3aa8-dc56-8dd71a7ce4a7 distance:10

console commands:
#Script.ReloadScripts()
#Script.UnloadScript("Scripts/Startup/main.lua")
#Script.ReloadScript("Scripts\\YourFile.lua")

#System.ClearConsole()
System.BrowseURL()

wh_ui_FPS = 1
wh_ui_FPS = 0

https://docs.cryengine.com/display/CEPROG/Console
DumpCommandsVars i_
https://docs.cryengine.com/display/CRYAUTOGEN/Home


cheat_eval cheat:logInfo(cheat:argsProcess("tokens:43590e12-70a9-3aa8-dc56-8dd71a7ce4a7,00d2d228-b63c-4aa7-8f18-dc9cf74ec97e", cheat.cheat_spawn_npc_args)["tokens"].value)

cheat_eval function test() System.LogAlways('asd') end
cheat_eval System.LogAlways("$3[INFO]$9 asd")

cheat_eval "function test2() local file=io.open('mods\\Cheat\\test.txt', "rb") local command=file:read("*all") file:close() System.LogAlways(command) end"

cheat_eval System.LogAlways(tostring(System.GetEntityByGUID("43590e12-70a9-3aa8-dc56-8dd71a7ce4a7")))

cheat_exec_file file:Data\test.txt

function cheat:clear_console()
	System.ClearConsole()
end

cheat_load_file file:Mods/Cheat/test.txt

C:\Program Files (x86)\Steam\steamapps\common\KingdomComeDeliverance\Data\Scripts\Scripts\Script\Soul.lua

#cheat:loadFile("mods/Cheat/test.txt", true)

#bootloader:load_file("mods/KCD_Bootloader/test.txt")
#clear_console()

#bootloader:load_file("mods/KCD_Bootloader/Scripts/cheat_util.lua")
#Script.ReloadScript("mods/KCD_Bootloader/Scripts/cheat_util.lua")

C:/Data/GIT/KCD-Cheat/Source/Scripts/Startup/main.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_util.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_localization.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_args.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_debug.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_stubs.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_console.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_buffs.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_factions.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_horses.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_items.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_merchants.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_perks.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_physics.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_picking.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_player.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_skills.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_quests.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_map.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_time.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_npc.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_weather.lua
C:/Data/GIT/KCD-Cheat/Source/Scripts/cheat_core_exec.lua