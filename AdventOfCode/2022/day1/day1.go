//https://adventofcode.com/2022/day/2

package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

const blank = -1

func readLines(path string) ([]int, error) {
    file, err := os.Open(path)
    if err != nil {
        return nil, err
    }
    defer file.Close()

    var lines []int
    scanner := bufio.NewScanner(file)
    for scanner.Scan() {
		numberValue, numberErr := strconv.Atoi(scanner.Text())
		if (numberErr == nil) {
			lines = append(lines, numberValue)
		} else {
			lines = append(lines, blank)
		}
    }
	lines = append(lines, blank)

    return lines, scanner.Err()
}


func main() {
    lines, _ := readLines("C:\\Source\\AdventOfCode\\Day1\\input.txt") // returns array of ints, with empty spaces replaced with -1

	// this is all terrible
	currentSum := 0
	maxSum := 0
	maxSum2 := 0
	maxSum3 := 0

	for _, value := range lines {
        if value != blank {
			currentSum += value
		} else {
			if (currentSum > maxSum) {

				if (maxSum != 0) {
					maxSum3 = maxSum2
					maxSum2 = maxSum
				}
				maxSum = currentSum 
			} else if (currentSum > maxSum2) {
				if (maxSum2 != 0) {
					maxSum3 = maxSum2
				}
				maxSum2 = currentSum
			} else if (currentSum > maxSum3) {

				maxSum3 = currentSum
			}

			currentSum = 0
		}
    }
	
	fmt.Println(maxSum + maxSum2 + maxSum3)
}