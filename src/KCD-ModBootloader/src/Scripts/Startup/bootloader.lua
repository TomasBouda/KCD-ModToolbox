bootloader={}

Script.ReloadScript("Scripts/bootloader_utils.lua")

function bootloader:load_file(filename)
	bootloader:logVerbose("Loading %s", tostring(filename))
	local chunk, err = loadfile(filename)
	if not err then
		if pcall(function() chunk() end) then
			bootloader:logVerbose("Loaded %s successfully", tostring(filename))
		else
			bootloader:logWarning("Some erros occured while loading %s", tostring(filename))
		end
	else
		bootloader:logError("Failed to load %s", tostring(filename))
	end
end

function bootloader:load_mods()
	bootloader:logDebug("Reading configuration file")

	local file = io.open('mods\\KCD_Bootloader\\config.txt', "r");
	if file == nil then return false end;
		
	while true do
		local filename = file:read("*line")
		if filename then
			print(filename)
			bootloader:load_file(filename)
		else
			break
		end
	end
	file:close();
		
	bootloader:logDebug("Configuration file loaded")
	return true;
end

function bootloader:get_command()
	local file = io.open('mods\\KCD_Bootloader\\invoke_command.txt', "rb");
	if file == nil then return false end;
	
	local command = file:read("*all");
	file:close();

	return command;
end

function bootloader:execute_command(command)
	local func = assert(loadstring(command));
	func();
end

function bootloader:execute_commands()
	
	local success, command = pcall(function() return bootloader:get_command() end)

	if not success then
		bootloader:logError("Failed to load command. %s", command)
	else
		if command ~= "" then
			success, message = pcall(function(cmd) return bootloader:execute_command(command) end);
			bootloader:logDebug("Command executed")

			if not success then
				bootloader:logError("Command execution failed!")
				bootloader:command_log("[FAILED] " .. tostring(message));
			else
				bootloader:logDebug("Command execution succeeeded.")
				bootloader:command_log("[SUCCESS] " .. tostring(command));
			end
		end
	end
end

function bootloader:clear_commands()
	local file = io.open('mods\\KCD_Bootloader\\invoke_command.txt', "w");
	if file == nil then return end;
	
	file:write("");
	file:close();
end

function bootloader:command_log(message)
	local logfile = io.open('mods\\KCD_Bootloader\\command_log.txt', "a");
	if logfile == nil then return {} end;
	
	logfile:write(message .. "\n");
	logfile:close();
end

function bootloader:timerCallback(nTimerId)
	bootloader:execute_commands()
	bootloader:clear_commands()

	Script.SetTimer(500, function(nTimerId) bootloader:timerCallback(nTimerId) end)
end

function bootloader:uiActionListener(actionName, eventName, argTable)
	if actionName == "sys_loadingimagescreen" and eventName == "OnEnd" then
		bootloader:timerCallback(0)
	end
end
UIAction.RegisterActionListener(bootloader, "", "", "uiActionListener")

-- function bootloader:uiEventSystemListener(actionName, eventName, argTable)
-- 	bootloader:timerCallback(0)
-- end
-- UIAction.RegisterEventSystemListener(bootloader, "", "", "uiEventSystemListener")

bootloader:load_mods()

bootloader:logInfo("KCD Bootloader initialized")