# amazon-interview
Interview question:

> Given a list of points on the XY plane, return the k points closest to the origin.</code>
> (I was asked later in the interview to remove any duplicates from the list.)

My solution was originally written in Java.
1) Use a hashset to remove duplicates. If a point has been visited, skip it. If not, add it to the hashset and results list.
2) Sort the results list by distnace to origin, ascending.
3) return sublist [0 - k].
4) Runs in O(n log n)

I recently found another way to do this using a priority queue that runs slightly faster.
1) Use a hashset to remove duplicates, same as above.
2) Create a Priority Queue, descending order, elements keyed to their distance from the origin.
3) for each Point p in given points:
    if p is smaller than largest point in the queue:
      dequeue top point.
      enqueue current item.
4) dequeue all points into new list.
5) results list will be in descending order so reverse it.
6) return results.
7) Runs in O(n log k).

When n is 10 million, results favor Priority queue very consistently. However, when n = 100 million, results are more mixed.

Comparison using <code>Process.GetCurrentProcess().TotalProcessorTime</code> where n = 100 million random points whose values range from [-100, 100) and k = 2500:

| Submitted     | Priority Queue | Comparison |
| ------------- |:--------------:|:------------------- |
| 11356.8728 ms | 11466.0735 ms  | 109.2007 ms slower  |
| 11497.2737 ms | 11575.2742 ms  | 78.0005 ms slower   |
| 12074.4774 ms | 11809.2757 ms  | 265.2017 ms faster  |
| 11824.8758 ms | 11622.0745 ms  | 202.8013 ms faster  |
| 12090.0775 ms | 11325.6726 ms  | 764.4049 ms faster  |
| 11809.2757 ms | 11294.4724 ms  | 514.8033 ms faster  |
| 12199.2782 ms | 15194.4974 ms  | 2995.2192 ms slower |
| 11434.8733 ms | 11294.4724 ms  | 140.4009 ms faster  |
| 12386.4794 ms | 11824.8758 ms  | 561.6036 ms faster  |
| 11980.8768 ms | 12168.0780 ms  | 187.2012 ms slower  |

Edit: Java and C# use different default sorting algorithms. Java uses a modified mergesort, while C# can use any one of three different algorithms based on the size of n:

(From [MSDN:](https://msdn.microsoft.com/en-us/library/6tf1f0bc(v=vs.110).aspx#Anchor_2))
> 1) If the partition size is fewer than 16 elements, it uses an insertion sort algorithm.
> 2) If the number of partitions exceeds 2 * LogN, where N is the range of the input array, it uses a Heapsort algorithm.
> 3) Otherwise, it uses a Quicksort algorithm.
