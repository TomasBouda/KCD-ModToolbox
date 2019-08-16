bootloader.log_level = 2
bootloader.log_level_verbose = 0
bootloader.log_level_debug = 1
bootloader.log_level_info = 2
bootloader.log_level_warning = 3
bootloader.log_level_error = 4

function bootloader:cls()
	System.ClearConsole()
end

function bootloader:reload_all()
	Script.ReloadScripts()
end

function bootloader:log(value)
	System.LogAlways(value)
	-- print(value)
end

function bootloader:setLogLevel(level)
	bootloader.log_level=level
end

function bootloader:logVerbose(message, ...)
	if bootloader.log_level_verbose >= bootloader.log_level then
		bootloader:log(string.format("$2[VERBOSE] ".. message, ...))
	end
end

function bootloader:logDebug(message, ...)
	if bootloader.log_level_debug >= bootloader.log_level then
		bootloader:log(string.format("$3[DEBUG] ".. message, ...))
	end
end

function bootloader:logInfo(message, ...)
	if bootloader.log_level_info >= bootloader.log_level then
		bootloader:log(string.format("$5[INFO] ".. message, ...))
	end
end

function bootloader:logWarning(message, ...)
	if bootloader.log_level_warning >= bootloader.log_level then
		bootloader:log(string.format("$6[WARNING] ".. message, ...))
	end
end

function bootloader:logError(message, ...)
	if bootloader.log_level_error >= bootloader.log_level then
		bootloader:log(string.format("$4[ERROR] ".. message, ...))
	end
end