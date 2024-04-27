extends GridMap


# Called when the node enters the scene tree for the first time.
func _ready():
	var arr = [];
	for i in range(100):
		for j in range(100):
			var rnd = abs(randi())%3
			for k in range(rnd):
				set_cell_item(Vector3(i, k, j), 0)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
var ir = 0
var jr = 0
var kr = 0
func _process(delta):
	var rnd = abs(randi())%3
	for k in range(3):
		set_cell_item(Vector3(ir, k, jr), -1)
	for k in range(rnd):
		set_cell_item(Vector3(ir, k, jr), 0)
	if jr == 0:
		kr = (kr+1)%100
		ir = 0
		jr = kr
	else:
		ir = (ir + 1)%100
		jr -= 1
	pass
