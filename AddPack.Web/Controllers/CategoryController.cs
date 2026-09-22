using Microsoft.AspNetCore.Mvc;

namespace AddPack.Web.Controllers;

public class CategoryController : Controller
{
    // Logika obługi kategorii wraz z przedmotami:
    // (zastosować potem także do serii)
    //
    // Usuwanie
    // - Usunięcie kategorii nigdy nie usuwa przedmiotów z nią powiązanych
    // - Usunięcie kategorii nie usuwa także podkategorii, ale podkategorie zmieniają nadkategorie (rodzica) na kategorię "Inne"
    // - Kategoria "Inne" podlega innym zasadom i nie może zostać usunięta, (następną kwestię jeszcze przemyśleć) ale może być wyłączona
    // - Przy kategorii najniższego poziomu (bez podkategorii) przedmioty powiązane zostają stricte przeniesione do kategorii "Inne"
    // 
    // Wyłączenie / archiwizacja
    // - Wyłączenie kategorii wyłącza także subkategorie i przedmioty z nią powiązane
    // - Wyłączone kategorie i przedmioty nie są widoczne w witrynie, ani dostępne dla zwykłego użytkownika

    public IActionResult Index()
    {
        return View();
    }
}
