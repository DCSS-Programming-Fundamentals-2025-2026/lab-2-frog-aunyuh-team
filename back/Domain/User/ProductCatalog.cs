using lab_1_frog_aunyuh_team.back.Domain.Core;

namespace lab_1_frog_aunyuh_team.back.Domain.User;

public class ProductCatalog
{
    public Product[] products { get; set; } = new Product[100];

    public void FixPrices()
    {
        for (int i = 0; i < products.Length; i++)
        {
            if (products[i] == null)
                continue;

            if (products[i].QuantityLeft.Value >= products[i].StockQuantity.Value * 0.9)
            {
                products[i].PricingAdjustmentState = PricingAdjustmentState.Discounted;
                products[i].CurrentPrice = new Money(products[i].BasePrice.Amount * (decimal)0.9);
            }
            else if (products[i].QuantityLeft.Value <= products[i].StockQuantity.Value * 0.4)
            {
                products[i].PricingAdjustmentState = PricingAdjustmentState.MarkedUp;
                products[i].CurrentPrice = new Money(products[i].BasePrice.Amount * (decimal)1.20);
            }
            else
            {
                products[i].PricingAdjustmentState = PricingAdjustmentState.None;
                products[i].CurrentPrice = new Money(products[i].BasePrice.Amount);
            }
        }
    }

    public Product[] SortByPriceAscending(ProductCategory pc)
    {
        Product[] array = CreateAndCount(pc);
        QuickSortAscending(array, 0, array.Length - 1);

        return array;
    }

    public Product[] SortByPriceDescending(ProductCategory pc)
    {
        Product[] array = CreateAndCount(pc);
        QuickSortDescending(array, 0, array.Length - 1);

        return array;
    }

    public Product[] SortByAlphabetical(ProductCategory pc)
    {
        Product[] array = CreateAndCount(pc);
        QuickSortAlphabetical(array, 0, array.Length - 1);

        return array;
    }

    private Product[] CreateAndCount(ProductCategory pc)
    {
        int count = 0;
        for (int i = 0; i < products.Length && products[i] != null; i++)
        {
            if (products[i].ProductCategory == pc)
                count++;
        }

        if (count == 0)
            throw new InvalidOperationException();

        Product[] array = new Product[count];
        for (int i = 0, j = 0; i < products.Length && products[i] != null; i++)
        {
            if (products[i].ProductCategory == pc)
                array[j++] = products[i];
        }

        return array;
    }

    private void QuickSortAscending(Product[] array, int l, int r)
    {
        if (l >= r) return; 

        Product pivot = array[(l + r) / 2]; 
        int i = l; 
        int j = r; 

        while (i <= j) 
        {
            while (i <= r && array[i].BasePrice.Amount < pivot.BasePrice.Amount)
                i++;

            while (j >= l && array[j].BasePrice.Amount > pivot.BasePrice.Amount)
                j--; 

            if (i <= j)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                j--;
            }
        }

        if (l < j)
            QuickSortAscending(array, l, j);

        if (i < r)
            QuickSortAscending(array, i, r);
    }

    private void QuickSortDescending(Product[] array, int l, int r)
    {
        if (l >= r) return;

        Product pivot = array[(l + r) / 2];
        int i = l;
        int j = r;

        while (i <= j)
        {
            while (i <= r && array[i].BasePrice.Amount > pivot.BasePrice.Amount)
                i++;

            while (j >= l && array[j].BasePrice.Amount < pivot.BasePrice.Amount)
                j--;

            if (i <= j)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                j--;
            }
        }

        if (l < j)
            QuickSortDescending(array, l, j);

        if (i < r)
            QuickSortDescending(array, i, r);
    }

    private void QuickSortAlphabetical(Product[] array, int l, int r)
    {
        if (l >= r) return;

        Product pivot = array[(l + r) / 2];
        int i = l;
        int j = r;

        while (i <= j)
        {
            while (i <= r && Compare1(array[i].Name, pivot.Name))
                i++;

            while (j >= l && Compare2(array[j].Name, pivot.Name))
                j--;

            if (i <= j)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                j--;
            }
        }

        if (l < j)
            QuickSortAlphabetical(array, l, j);

        if (i < r)
            QuickSortAlphabetical(array, i, r);
    }

    private bool Compare1(string wordArray, string wordPivot)
    {
        for (int i = 0; i < wordArray.Length && i < wordPivot.Length; i++)
        {
            if (wordArray[i] < wordPivot[i])
                return true;
            else if (wordArray[i] > wordPivot[i])
                return false;
        }

        if (wordArray.Length < wordPivot.Length)
            return true;

        return false;
    }

    private bool Compare2(string wordArray, string wordPivot)
    {
        for (int i = 0; i < wordArray.Length && i < wordPivot.Length; i++)
        {
            if (wordArray[i] > wordPivot[i])
                return true;
            else if (wordArray[i] < wordPivot[i])
                return false;
        }

        if (wordArray.Length > wordPivot.Length)
            return true;

        return false;
    }
}