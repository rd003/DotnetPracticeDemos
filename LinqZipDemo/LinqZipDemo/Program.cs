// Example 1
// int[] nums1 = [1, 2, 3, 4];
// int[] nums2 = [3, 4, 5, 6];

// IEnumerable<int>? product = nums1.Zip(nums2, (n1, n2) => n1 * n2);

// Console.WriteLine(string.Join(", ", product));  // 3, 8, 15, 24


// Example 2
// string[] students = ["John", "Mike", "Tim"];
// int[] testScores = [70, 80, 90];

// var studentGrades = students.Zip(testScores, (student, score) =>
//                                              new StudetGrade(
//                                                 student,
//                                                 score,
//                                                 score >= 90 ? "A" : "B")
//                                              );

// foreach (var sg in studentGrades)
// {
//     Console.WriteLine(sg.ToString());
// }

// // Model for displaying student grades
// public record StudetGrade(string Student, int Score, string Grade);

// Caveat

// string[] letters = ["A", "B", "C", "D"];

// int[] nums = [1, 2, 3];

// var sequence = letters.Zip(nums, (l, n) => $"{l}-{n}");

// Console.WriteLine(string.Join(", ", sequence));  // A-1, B-2, C-3

// with more than 2 enumarables
var list1 = new List<int> { 1, 2, 3 };
var list2 = new List<char> { 'A', 'B', 'C' };
var list3 = new List<string> { "One", "Two", "Three" };

var zipped = list1.Zip(list2, (num, letter) => new { num, letter })
                  .Zip(list3, (pair, word) => new { pair.num, pair.letter, word });

foreach (var item in zipped)
{
    Console.WriteLine($"{item.num}-{item.letter}-{item.word}");
}

/* output 

1-A-One
2-B-Two
3-C-Three

*/