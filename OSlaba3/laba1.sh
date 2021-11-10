folders=$(ls -d */ | sort -g)
rm result.txt
for i in $folders; do
    files=$(find $i -type f)
    for j in $files; do
        cat $j >> result.txt
    done
done