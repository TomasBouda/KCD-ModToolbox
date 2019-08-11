bootloader={}

function bootloader:load_file(filename)
	System.LogAlways("$2Loading " .. tostring(filename))
	local chunk, err = loadfile(filename)
    if not err then
		if pcall(function() chunk() end) then
			System.LogAlways("$2Loaded " .. tostring(filename) .. " successfully")
			-- print("ok")
		else
			System.LogAlways("$6Some erros occured while loading " .. tostring(filename))
			-- print("nok")
		end
    else
		System.LogAlways("$3Failed to load " .. tostring(filename))
		-- print("bnok")
    end
end

function bootloader:reload_all()
	Script.ReloadScripts()
end

function bootloader:load_config()
	System.LogAlways("$5Reading configuration file")

	local file = io.open('mods\\KCD_Bootloader\\config.txt', "r");
	-- local file = io.open('c:\\Program Files (x86)\\Steam\\steamapps\\common\\KingdomComeDeliverance\\mods\\KCD_Bootloader\\config.txt', "r");
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
		
	System.LogAlways("$3Configuration file loaded")
	return true;
end

function cls()
	System.ClearConsole()
end

System.LogAlways("$3KCD Bootloader initialized")

bootloader:load_config()
