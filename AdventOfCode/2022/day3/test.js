
// part 1
let input = `${require("fs").readFileSync("input.txt")}`.split("\r\n");
input.forEach(x => {
    x.trim()
})



results = [];
for (const i of input) {
    console.log(i.length)
    console.log(JSON.stringify(i))
    let firstHalf = i.substring(0, i.length / 2)
    let sHalf = i.substring(i.length / 2);
    let fArray = firstHalf.split('');
    let sArray = sHalf.split('')
    let fSet = new Set(fArray);
    let sSet = new Set(sArray)

    console.log(fSet)
    console.log(sSet)

    inter  = intersection(fSet, sSet)
    console.log(inter)
    results.push(...inter)
}

console.log(results)

resultsSum = 0;

for (let index = 0; index < results.length; index++) {
    const element = results[index];
    if (element == element.toUpperCase())
    {
        resultsSum += element.toLowerCase().charCodeAt(0) - 96 + 26
    } else {
        resultsSum += element.charCodeAt(0) - 96;
    }
}

console.log(resultsSum)



function intersection(setA, setB) {
    const result = [];

    for (const elem of setA) {
        if (setB.has(elem)) {
            result.push(elem);
        }
    }

    return result
}
