if ./ProtoGen ./*.proto; then
	echo build success
else
	echo build failed
	read -p "press any key to quit" -n 1 -r
fi