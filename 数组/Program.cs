using System.Numerics;
using System.Runtime.Intrinsics;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static void Main()
    {
        //Console.WriteLine(_704二分查找.Search(new int[] { -1, 0, 3, 5, 9, 12 }, 2));

        //Console.WriteLine(_35搜索插入位置.SearchInsert(new int[] { 1, 3, 5, 6 }, 7));

        //_34在排序数组中查找元素的第一个和最后一个位置.SearchRange(new int[] { 5, 7, 7, 8, 8, 10 }, 6);

        //Console.WriteLine(_69x的平方根.MySqrt(115141));

        Console.WriteLine(_367有效的完全平方数.IsPerfectSquare(2147483647));
    }
}

public class _704二分查找
{
    public static int Search(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1;// 定义target在左闭右闭的区间里，[left，right]
        while (left <= right)// 当left=right，区间[left，right]依然有效，所以用 <=
        {
            int middle = left + (right - left) / 2;//防止溢出 等同于 (left+right)/2
            if (nums[middle] > target)
            {
                right = middle - 1;// target 在左区间，所以[left, middle - 1]
            }
            else if (nums[middle] < target)
            {
                left = middle + 1;// target 在右区间，所以[middle + 1, right]
            }
            else// nums[middle] == target
            {
                return middle; // 数组中找到目标值，直接返回下标
            }
        }
        // 未找到目标值
        return -1;
    }
}

public class _35搜索插入位置
{
    public static int SearchInsert(int[] nums, int target)
    {
        int n = nums.Length;
        int left = 0;
        int right = n - 1; // 定义target在左闭右闭的区间里，[left, right]
        while (left <= right)
        { // 当left==right，区间[left, right]依然有效
            int middle = left + ((right - left) / 2);// 防止溢出 等同于(left + right)/2
            if (nums[middle] > target)
            {
                right = middle - 1; // target 在左区间，所以[left, middle - 1]
            }
            else if (nums[middle] < target)
            {
                left = middle + 1; // target 在右区间，所以[middle + 1, right]
            }
            else
            { // nums[middle] == target
                return middle;
            }
        }
        // 分别处理如下四种情况
        // 目标值在数组所有元素之前  [0, -1]
        // 目标值等于数组中某一个元素  return middle;
        // 目标值插入数组中的位置 [left, right]，return  right + 1
        // 目标值在数组所有元素之后的情况 [left, right]， 因为是右闭区间，所以 return right + 1
        return right + 1;
    }
}

public class _34在排序数组中查找元素的第一个和最后一个位置
{
    public static int[] SearchRange(int[] nums, int target)
    {
        int[] result = new int[2] { -1, -1 };
        int leftBorder = getLeftBorder(nums, target);
        int rightBorder = getRightBorder(nums, target);
        // 情况一
        if (leftBorder == -2 || rightBorder == -2) return result;
        // 情况三
        if (rightBorder - leftBorder > 1)
        {
            result[0] = leftBorder + 1;
            result[1] = rightBorder - 1;
            return result;
        }
        // 情况二
        return result;
    }

    // 二分查找，寻找target的右边界（不包括target）
    // 如果rightBorder为没有被赋值（即target在数组范围的左边，例如数组[3,3]，target为2），为了处理情况一
    public static int getRightBorder(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1; // 定义target在左闭右闭的区间里，[left, right]
        int rightBorder = -2; // 记录一下rightBorder没有被赋值的情况
        while (left <= right)
        { // 当left==right，区间[left, right]依然有效
            int middle = left + ((right - left) / 2);// 防止溢出 等同于(left + right)/2
            if (nums[middle] > target)
            {
                right = middle - 1; // target 在左区间，所以[left, middle - 1]
            }
            else
            { // 当nums[middle] == target的时候，更新left，这样才能得到target的右边界
                left = middle + 1;
                rightBorder = left;
            }
        }
        return rightBorder;
    }

    // 二分查找，寻找target的左边界leftBorder（不包括target）
    // 如果leftBorder没有被赋值（即target在数组范围的右边，例如数组[3,3],target为4），为了处理情况一
    public static int getLeftBorder(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1; // 定义target在左闭右闭的区间里，[left, right]
        int leftBorder = -2; // 记录一下leftBorder没有被赋值的情况
        while (left <= right)
        {
            int middle = left + ((right - left) / 2);
            if (nums[middle] >= target)
            { // 寻找左边界，就要在nums[middle] == target的时候更新right
                right = middle - 1;
                leftBorder = right;
            }
            else
            {
                left = middle + 1;
            }
        }
        return leftBorder;
    }
}

public class _69x的平方根
{
    public static int MySqrt(int x)
    {
        int left = 0, right = x;
        int middle = left + (right - left) / 2;
        int result = middle;
        while (left <= right)
        {
            middle = left + (right - left) / 2;
            if ((uint)middle * middle > x)
            {
                right = middle - 1;
                result = right;
            }
            else if ((uint)middle * middle < x)
            {
                left = middle + 1;
                result = left;
            }
            else
            {
                return middle;
            }

            if ((uint)(middle + 1) * (middle + 1) > x && (uint)(middle) * (middle) < x)
                return middle;
        }

        return result;
    }
}

public class _367有效的完全平方数
{
    public static bool IsPerfectSquare(int num)
    {
        int left = 0, right = num;
        while (left <= right)
        {
            int middle = left + (right - left) / 2;

            if ((long)middle * middle > num)
            {
                right = middle - 1;
            }
            else if ((long)middle * middle < num)
            {
                left = left + 1;
            }
            else
                return true;
        }

        return false;
    }
}