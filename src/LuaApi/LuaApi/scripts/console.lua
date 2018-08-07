function write(output)
	return console:APIWrite(tostring(output))
end

--don't delete, used for Testing
function getOutput(output)
	return "Hello " .. output .. "!"
end