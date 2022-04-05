folders=$(ls -d */ | sort -g)
rm result.txt
k="\n"
for i in $folders; do
    files=$(find $i -type f)
    for j in $files; do
        cat $j >> result.txt
        echo >>result.txt
    done
done