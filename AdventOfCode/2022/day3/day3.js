//https://adventofcode.com/2022/day/3

let input = `${require("fs").readFileSync("input.txt")}`.split("\r\n");
input.forEach(x => {
    x.trim()
})


// Part 1
// results = [];
// for (const i of input) {
//     console.log(i.length)
//     console.log(JSON.stringify(i))
//     let firstHalf = i.substring(0, i.length / 2)
//     let sHalf = i.substring(i.length / 2);
//     let fArray = firstHalf.split('');
//     let sArray = sHalf.split('')
//     let fSet = new Set(fArray);
//     let sSet = new Set(sArray)

//     console.log(fSet)
//     console.log(sSet)

//     inter  = intersection(fSet, sSet)
//     console.log(inter)
//     results.push(...inter)
// }

// console.log(results)

// resultsSum = 0;



// for (let index = 0; index < results.length; index++) {
//     const element = results[index];
//     if (element == element.toUpperCase())
//     {
//         resultsSum += element.toLowerCase().charCodeAt(0) - 96 + 26
//     } else {
//         resultsSum += element.charCodeAt(0) - 96;
//     }
// }

// console.log(resultsSum)

// Part 2
let r = [];
const chunkSize = 3;
for (let i = 0; i < input.length; i += chunkSize) {
    const chunk = input.slice(i, i + chunkSize);
    console.log(chunk)

    let sets = chunk.map(x => new Set(x.split('')))
    const intersect =  intersection(sets[2], intersection(sets[0], sets[1]))

    r.push(...[...intersect])
}

resultsSum = 0;

for (let index = 0; index < r.length; index++) {
    const element = r[index];
    if (element == element.toUpperCase())
    {
        resultsSum += element.toLowerCase().charCodeAt(0) - 96 + 26
    } else {
        resultsSum += element.charCodeAt(0) - 96;
    }
}

console.log(resultsSum)

// helper method
function intersection(setA, setB) {
    const result = new Set();

    for (const elem of setA) {
        if (setB.has(elem)) {
            result.add(elem);
        }
    }

    return result
}
