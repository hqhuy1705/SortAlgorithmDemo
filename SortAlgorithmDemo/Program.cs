// See https://aka.ms/new-console-template for more information

Console.WriteLine($"{Environment.NewLine}Bubble Sort");
PerformSort(BubbleSort);

Console.WriteLine($"{Environment.NewLine}Insertion Sort");
PerformSort(InsertionSort);

Console.WriteLine($"{Environment.NewLine}Selection Sort");
PerformSort(SelectionSort);

Console.WriteLine($"{Environment.NewLine}Shell Sort");
PerformSort(ShellSort);

Console.WriteLine($"{Environment.NewLine}Quick Sort");
PerformSortWithStartEnd(QuickSort);

Console.WriteLine($"{Environment.NewLine}Heap Sort");
PerformSort(HeapSort);

Console.WriteLine($"{Environment.NewLine}Merge Sort");
PerformSortWithStartEnd(MergeSort);

Console.WriteLine($"{Environment.NewLine}Count Sort");
PerformSort(CountSortNegativeNumbers);

Console.WriteLine($"{Environment.NewLine}Bucket Sort");
PerformSort(BucketSort);

#region Bubble Sort

/// <summary>
/// Bubble Sort
/// Best | Average | Worst
/// Ω(n)    θ(n^2)	 O(n^2)
/// </summary>
static void BubbleSort(int[] arr)
{
    // An optimized version of Bubble Sort
    int i, j;
    bool swapped;
    int n = arr.Length;

    for (i = 0; i < n - 1; i++)
    {
        swapped = false;

        for (j = 0; j < n - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                // swap arr[j] and arr[j+1]
                (arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
                swapped = true;
            }
        }

        // IF no two elements were
        // swapped by inner loop, then break
        if (swapped == false) break;
    }
}

#endregion

#region Insertion Sort

/// <summary>
/// Insertion Sort
/// Best | Average | Worst
/// Ω(n)    θ(n^2)	 O(n^2)
/// </summary>
static void InsertionSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n; i++)
    {
        var item = arr[i];
        var currentIndex = i;

        // Move elements of arr[0..i-1],
        // that are greater than key,
        // to one position ahead of
        // their current position
        while (currentIndex > 0 && arr[currentIndex - 1] > item)
        {
            arr[currentIndex] = arr[currentIndex - 1];
            currentIndex--;
        }

        arr[currentIndex] = item;
    }
}

#endregion

#region Selection Sort

/// <summary>
/// Selection Sort
/// Best   | Average | Worst
/// Ω(n^2)	 θ(n^2)   O(n^2)
/// </summary>
static void SelectionSort(int[] arr)
{
    int n = arr.Length;
    for (var i = 0; i < n; i++)
    {
        // Find the minimum element in unsorted array
        var min = i;
        for (var j = i + 1; j < n; j++)
        {
            if (arr[min] > arr[j])
            {
                min = j;
            }
        }

        if (min != i)
        {
            // Swap the found minimum element with the first
            (arr[i], arr[min]) = (arr[min], arr[i]);
        }
    }
}

#endregion

#region Shell Sort

/// <summary>
/// Shell Sort
/// Best   | Average |   Worst
/// Ω(n)	Ω(nlogn)	Ω(nlogn)
/// </summary>
static void ShellSort(int[] arr)
{
    int i, j, pos, temp;
    int n = arr.Length;
    pos = 3;
    while (pos > 0)
    {
        for (i = 0; i < n; i++)
        {
            j = i;
            temp = arr[i];
            while ((j >= pos) && (arr[j - pos] > temp))
            {
                arr[j] = arr[j - pos];
                j -= pos;
            }
            arr[j] = temp;
        }

        if (pos / 2 != 0)
            pos /= 2;
        else if (pos == 1)
            pos = 0;
        else
            pos = 1;
    }
}

#endregion

#region Quick Sort

/// <summary>
/// Quick Sort
/// The main function that implements QuickSort
/// arr[] --> Array to be sorted,
/// low --> Starting index,
/// high --> Ending index
/// 
/// Best        |    Average     |  Worst
/// Ω(n log(n))	    θ(n log(n))	    O(n^2)
/// </summary>
static void QuickSort(int[] arr, int low, int high)
{
    if (low < high)
    {

        // pi is partitioning index, arr[p]
        // is now at right place
        int pi = Partition(arr, low, high);

        // Separately sort elements before
        // partition and after partition
        QuickSort(arr, low, pi - 1);
        QuickSort(arr, pi + 1, high);
    }
}

// A utility function to swap two elements
static void Swap(int[] arr, int i, int j)
{
    (arr[j], arr[i]) = (arr[i], arr[j]);
}

/* This function takes last element as pivot, places
     the pivot element at its correct position in sorted
     array, and places all smaller (smaller than pivot)
     to left of pivot and all greater elements to right
     of pivot */
static int Partition(int[] arr, int low, int high)
{
    // pivot
    int pivot = arr[high];

    // Index of smaller element and
    // indicates the right position
    // of pivot found so far
    int i = (low - 1);

    for (int j = low; j <= high - 1; j++)
    {

        // If current element is smaller
        // than the pivot
        if (arr[j] < pivot)
        {

            // Increment index of
            // smaller element
            i++;
            Swap(arr, i, j);
        }
    }

    Swap(arr, i + 1, high);

    return (i + 1);
}

#endregion

#region Heap sort

/// <summary>
/// HeapSort
/// 
/// Best        |    Average     |  Worst
/// Ω(n log(n))	   θ(n log(n))	   O(n log(n))
/// </summary>
static void HeapSort(int[] arr)
{
    int n = arr.Length;

    // Build heap (rearrange array)
    for (int i = n / 2 - 1; i >= 0; i--)
        Heapify(arr, n, i);

    // One by one extract an element from heap
    for (int i = n - 1; i > 0; i--)
    {
        // Move current root to end
        (arr[i], arr[0]) = (arr[0], arr[i]);

        // call max heapify on the reduced heap
        Heapify(arr, i, 0);
    }
}

// To heapify a subtree rooted with node i which is
// an index in arr[]. n is size of heap
static void Heapify(int[] arr, int n, int i)
{
    int largest = i; // Initialize largest as root
    int l = 2 * i + 1; // left = 2*i + 1
    int r = 2 * i + 2; // right = 2*i + 2

    // If left child is larger than root
    if (l < n && arr[l] > arr[largest])
        largest = l;

    // If right child is larger than largest so far
    if (r < n && arr[r] > arr[largest])
        largest = r;

    // If largest is not root
    if (largest != i)
    {
        (arr[largest], arr[i]) = (arr[i], arr[largest]);

        // Recursively heapify the affected sub-tree
        Heapify(arr, n, largest);
    }
}

#endregion

#region Merge Sort

/// <summary>
/// Main function that
/// sorts arr[l..r]
/// 
/// Best        |    Average     |  Worst
/// Ω(n log(n))	    θ(n log(n))	    O(n log(n))
/// </summary>
static void MergeSort(int[] arr, int l, int r)
{
    if (l < r)
    {
        // Find the middle
        // point
        int m = l + (r - l) / 2;

        // Sort first and
        // second halves
        MergeSort(arr, l, m);
        MergeSort(arr, m + 1, r);

        // Merge the sorted halves
        MergeArr(arr, l, m, r);
    }
}

// Merges two subarrays of []arr.
// First subarray is arr[l..m]
// Second subarray is arr[m+1..r]
static void MergeArr(int[] arr, int l, int m, int r)
{
    // Find sizes of two
    // subarrays to be merged
    int n1 = m - l + 1;
    int n2 = r - m;

    // Create temp arrays
    int[] L = new int[n1];
    int[] R = new int[n2];
    int i, j;

    // Copy data to temp arrays
    for (i = 0; i < n1; ++i)
        L[i] = arr[l + i];
    for (j = 0; j < n2; ++j)
        R[j] = arr[m + 1 + j];

    // Merge the temp arrays

    // Initial indexes of first
    // and second subarrays
    i = 0;
    j = 0;

    // Initial index of merged
    // subarray array
    int k = l;
    while (i < n1 && j < n2)
    {
        if (L[i] <= R[j])
        {
            arr[k] = L[i];
            i++;
        }
        else
        {
            arr[k] = R[j];
            j++;
        }
        k++;
    }

    // Copy remaining elements
    // of L[] if any
    while (i < n1)
    {
        arr[k] = L[i];
        i++;
        k++;
    }

    // Copy remaining elements
    // of R[] if any
    while (j < n2)
    {
        arr[k] = R[j];
        j++;
        k++;
    }
}

#endregion

#region Count Sort

/// <summary>
/// CountSort
/// Best   | Average |  Worst
/// Ω(n+k)	 θ(n+k)	    O(n+k)
/// </summary>
static void CountSort(char[] arr)
{
    int n = arr.Length;

    // The output character array that
    // will have sorted arr
    char[] output = new char[n];

    // Create a count array to store
    // count of individual characters
    // and initialize count array as 0
    int[] count = new int[256];

    for (int i = 0; i < 256; ++i)
        count[i] = 0;

    // store count of each character
    for (int i = 0; i < n; ++i)
        ++count[arr[i]];

    // Change count[i] so that count[i]
    // now contains actual position of
    // this character in output array
    for (int i = 1; i <= 255; ++i)
        count[i] += count[i - 1];

    // Build the output character array
    // To make it stable we are operating in reverse order.
    for (int i = n - 1; i >= 0; i--)
    {
        output[count[arr[i]] - 1] = arr[i];
        --count[arr[i]];
    }

    // Copy the output array to arr, so
    // that arr now contains sorted
    // characters
    for (int i = 0; i < n; ++i)
        arr[i] = output[i];
}

/// <summary>
/// Counting sort which takes negative numbers as well
/// 
/// Best   | Average |  Worst
/// Ω(n+k)	 θ(n+k)	    O(n+k)
/// </summary>
static void CountSortNegativeNumbers(int[] arr)
{
    int max = arr.Max();
    int min = arr.Min();
    int range = max - min + 1;
    int[] count = new int[range];
    int[] output = new int[arr.Length];

    for (int i = 0; i < arr.Length; i++)
    {
        count[arr[i] - min]++;
    }

    for (int i = 1; i < count.Length; i++)
    {
        count[i] += count[i - 1];
    }

    for (int i = arr.Length - 1; i >= 0; i--)
    {
        output[count[arr[i] - min] - 1] = arr[i];
        count[arr[i] - min]--;
    }

    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = output[i];
    }
}

#endregion

#region Bucket Sort

/// <summary>
/// Function to sort arr[]
/// using bucket sort
/// 
/// Best   | Average |  Worst
/// Ω(n+k)	 θ(n+k)	    O(n^2)
/// </summary>
static void BucketSort(int[] arr)
{
    int arrLength = arr.Length;
    int minValue = arr.Min();
    int maxValue = arr.Max();

    List<int>[] bucket = new List<int>[maxValue - minValue + 1];
    int bucketLenght = bucket.Length;

    for (int i = 0; i < bucketLenght; i++)
    {
        bucket[i] = new List<int>();
    }

    for (int i = 0; i < arrLength; i++)
    {
        bucket[arr[i] - minValue].Add(arr[i]);
    }

    int k = 0;
    for (int i = 0; i < bucketLenght; i++)
    {
        if (bucket[i].Count > 0)
        {
            for (int j = 0; j < bucket[i].Count; j++)
            {
                arr[k] = bucket[i][j];
                k++;
            }
        }
    }
}

#endregion

#region PerformSort

static void PerformSort(Action<int[]> sortFunction)
{
    var arrInt = GetRandomArray(100);

    DiplayArray(arrInt);

    DateTime start = DateTime.Now;

    sortFunction(arrInt);

    DateTime end = DateTime.Now;

    Console.WriteLine("Result:");
    DiplayArray(arrInt);

    TimeSpan ts = (end - start);
    Console.WriteLine($"Function {sortFunction.Method.Name} {Environment.NewLine} Time execution: {ts.TotalMilliseconds} ms");
    Console.WriteLine("-----------------------------------------------------");
}

static void PerformSortWithStartEnd(Action<int[], int, int> sortFunction)
{
    var arrInt = GetRandomArray(100);

    DiplayArray(arrInt);

    DateTime start = DateTime.Now;

    sortFunction(arrInt, 0, arrInt.Length - 1);

    DateTime end = DateTime.Now;
    
    Console.WriteLine("Result:");
    DiplayArray(arrInt);

    TimeSpan ts = (end - start);
    Console.WriteLine($"Function {sortFunction.Method.Name} {Environment.NewLine} Time execution: {ts.TotalMilliseconds} ms");
    Console.WriteLine("-----------------------------------------------------");
}

static void DiplayArray(int[] arr)
{
    Console.WriteLine("-----------------------------------------------------");
    Console.WriteLine(String.Join(", ", arr));
    Console.WriteLine("-----------------------------------------------------");
}

static int[] GetRandomArray(int length)
{
    int[] arr = new int[length];
    Random random = new();

    for (int i = 0; i < length; i++)
    {
        arr[i] = random.Next(-1000, 1000);
    }

    return arr;
}

#endregion
