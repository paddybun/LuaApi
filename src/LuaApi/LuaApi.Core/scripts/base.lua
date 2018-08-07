clock = os.clock
console = nil
runner = nil
lua = nil
data = nil

function sleep(n)
	local timer = clock()
	while clock() - timer <= n do end
end