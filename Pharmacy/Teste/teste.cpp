//
// Created by Știube Denis on 26.05.2024.
//

#include "teste.h"

void testDomain(){
    Medicament m1{"d1", 45.3, "p1", "s1"};
    assert(m1.getDenumire() == "d1");
    assert(m1.getPret() == 45.3);
    assert(m1.getProducator() == "p1");
    assert(m1.getSubst() == "s1");
    m1.setPret(55);
    assert(m1.getPret() == 55);
    m1.setSubst("s10");
    assert(m1.getSubst() == "s10");
}
void testAddRepo(){
    Repo repo;
    assert(repo.getSize() == 0);
    Medicament m1{"d1", 50, "p1", "s1"};
    repo.addMedicament(m1);
    try{
        repo.addMedicament(m1);
        assert(false);
    }
    catch (Error& error){
        assert(true);
        assert(error.getMesaj() == "Medicamentul exista deja sau nu este valid!");
    }
    assert(repo.getSize() == 1);
    assert(repo.getAll()[0].getPret() == 50);
}

void testDeleteRepo(){
    Repo repo;
    assert(repo.getSize() == 0);
    Medicament m1{"d1", 50, "p1", "s1"};
    repo.addMedicament(m1);
    assert(repo.getSize() == 1);
    repo.deleteMedicament(m1);
    assert(repo.getSize() == 0);
}

void testFindRepo(){
    Repo repo;
    assert(repo.getSize() == 0);
    Medicament m1{"d1", 50, "p1", "s1"};
    repo.addMedicament(m1);
    assert(repo.getSize() == 1);
    assert(repo.findMedicament("d1", "p1"));
    assert(!repo.findMedicament("d2", "p3"));
}

void testUpdateRepo(){
    Repo repo;
    assert(repo.getSize() == 0);
    Medicament m1{"d1", 50, "p1", "s1"};
    repo.addMedicament(m1);
    assert(repo.getSize() == 1);
    Medicament m2{"d1", 75.45, "p1", "s1"};
    repo.updateMedicament(m1, m2);
    assert(repo.getAll()[0].getPret() == 75.45);
    m1.setSubst("s2");
    repo.updateMedicament(m2, m1);
    assert(repo.getAll()[0].getSubst() == "s2" && repo.getAll()[0].getPret() == 50);
}

void testGetSizeRepo(){
    Repo repo;
    assert(repo.getSize() == 0);
    Medicament m1{"d1", 50, "p1", "s1"};
    repo.addMedicament(m1);
    Medicament m2{"d2", 100, "p2", "s2"};
    repo.addMedicament(m2);
    assert(repo.getSize() == 2);
}

void testGetAllRepo(){
    Repo repo;
    assert(repo.getSize() == 0);
    Medicament m1{"d1", 50, "p1", "s1"};
    repo.addMedicament(m1);
    Medicament m2{"d2", 100, "p2", "s2"};
    repo.addMedicament(m2);
    assert(repo.getSize() == 2);
    vector<Medicament> medicamente = repo.getAll();
    assert(medicamente.size() == 2);
    assert(medicamente[0].getPret() == 50);
    assert(medicamente[1].getProducator() == "p2");
}

void testAddInFileRepo(){
    InFileRepo repoF("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
    assert(repoF.getSize() == 0);
    Medicament m1{"d1", 200, "p1", "s2"};
    repoF.addMedicament(m1);
    assert(repoF.getSize() == 1);
    try {
        repoF.addMedicament(m1);
        assert(false);
    }
    catch (Error& err){
        assert(err.getMesaj() == "Medicamentul exista deja sau nu este valid!");
    }
    Medicament m2{"d3", 45, "p4", "s2"};
    repoF.addMedicament(m2);
    assert(repoF.getSize() == 2);
    assert(repoF.getAll()[0] == m1);
    assert(repoF.getAll()[1] == m2);
    clearFile("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
}

void testDeleteInFileRepo(){
    InFileRepo repoF("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
    assert(repoF.getSize() == 0);
    Medicament m1{"d1", 200, "p1", "s2"};
    repoF.addMedicament(m1);
    assert(repoF.getSize() == 1);
    Medicament m2{"d3", 45, "p4", "s2"};
    repoF.addMedicament(m2);
    assert(repoF.getSize() == 2);
    repoF.deleteMedicament(m1);
    assert(repoF.getSize() == 1);
    assert(repoF.getAll()[0] == m2);
    Medicament m3{"d10", 24, "p3", "s7"};
    try{
        repoF.deleteMedicament(m3);
        assert(false);
    }
    catch (Error& err){
        assert(err.getMesaj() == "Medicamentul nu exista sau nu este valid!");
    }
    clearFile("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
}

void testUpdateInFileRepo(){
    InFileRepo repoF("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
    assert(repoF.getSize() == 0);
    Medicament m1{"d1", 200, "p1", "s2"};
    repoF.addMedicament(m1);
    assert(repoF.getSize() == 1);
    assert(repoF.getAll()[0] == m1);
    Medicament m2{"d3", 45, "p4", "s2"};
    repoF.updateMedicament(m1, m2);
    assert(repoF.getSize() == 1);
    assert(repoF.getAll()[0] == m2);
    try{
        repoF.updateMedicament(m1, m2);
        assert(false);
    }
    catch (Error& err){
        assert(err.getMesaj() == "Medicamentul nu exista sau nu este valid!");
    }
    clearFile("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
}

void testRepo(){
    testAddRepo();
    testDeleteRepo();
    testFindRepo();
    testUpdateRepo();
    testGetAllRepo();
    testGetSizeRepo();
}

void testInFileRepo(){
    testAddInFileRepo();
    testDeleteInFileRepo();
    testUpdateInFileRepo();
}

void testAddReteta(){
    Reteta r;
    assert(!r.getSize());
    Medicament m1{"d1", 100, "p1", "s1"};
    r.addMedicament(m1);
    assert(r.getSize() == 1);
    r.addMedicament(m1);
    assert(r.getSize() == 2);
}

void testDeleteAllReteta(){
    Reteta r;
    assert(!r.getSize());
    Medicament m1{"d1", 100, "p1", "s1"};
    r.addMedicament(m1);
    assert(r.getSize() == 1);
    r.addMedicament(m1);
    assert(r.getSize() == 2);
    Medicament m2{"d2", 200, "p1", "s2"};
    r.addMedicament(m2);
    r.deleteAll();
    assert(r.getSize() == 0);
}

void testGetAllReteta(){
    Reteta r;
    assert(!r.getSize());
    Medicament m1{"d1", 100, "p1", "s1"};
    r.addMedicament(m1);
    assert(r.getSize() == 1);
    r.addMedicament(m1);
    assert(r.getSize() == 2);
    Medicament m2{"d2", 200, "p1", "s2"};
    r.addMedicament(m2);
    vector<Medicament> medicamente = r.getAll();
    assert(medicamente.size() == 3);
    assert(medicamente[0] == m1 && medicamente[2] == m2);
}

void testReteta(){
    testAddReteta();
    testDeleteAllReteta();
    testGetAllReteta();
}

void testAddFarmacieService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    assert(service.getSize() == 1);
    service.addMedicamentFarmacie("d2", 45.25, "p2", "s2");
    assert(service.getSize() == 2);
    assert(service.getAll()[1].getDenumire() == "d2");
}

void testDeleteFarmacieService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    assert(service.getSize() == 1);
    assert(service.getAll()[0].getSubst() == "s1");
    try{
        service.deleteMedicamentFarmacie("d3", "p10");
        assert(false);
    }
    catch (Error& error){
        assert(true);
        assert(error.getMesaj() == "Medicamentul nu exista sau nu este valid!");
    }
    service.deleteMedicamentFarmacie("d1", "p1");
    assert(service.getSize() == 0);
}

void testUpdatePretService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    assert(service.getSize() == 1);
    assert(service.getAll()[0].getPret() == 67.23);
    try{
        service.updatePret("d1", "p10", 500);
        assert(false);
    }
    catch (Error& error){
        assert(true);
        assert(error.getMesaj() == "Medicamentul nu exista sau nu este valid!");
    }
    service.updatePret("d1", "p1", 70);
    assert(service.getAll()[0].getPret() == 70);
}

void testUpdateSubstService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    assert(service.getSize() == 1);
    assert(service.getAll()[0].getSubst() == "s1");
    try{
        service.updateSubst("d2", "p10", "s10");
        assert(false);
    }
    catch (Error& error){
        assert(true);
        assert(error.getMesaj() == "Medicamentul nu exista sau nu este valid!");
    }
    service.updateSubst("d1", "p1", "s3");
    assert(service.getAll()[0].getSubst() == "s3");
}

void testFindMedicament(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    assert(service.getSize() == 1);
    assert(service.findMedicament("d1", "p1") == true);
    assert(service.findMedicament("10", "pharma") == false);
}

void testFiltruPretService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 50, "p1", "s1");
    assert(service.getSize() == 1);
    service.addMedicamentFarmacie("d2", 100, "p2", "s2");
    service.addMedicamentFarmacie("d3", 50, "p7", "s8");
    assert(service.getSize() == 3);
    vector<Medicament> listaFiltrata = service.filtruPret(50);
    assert(listaFiltrata.size() == 2);
    assert(service.getSize() == 3);
    assert(listaFiltrata[0].getDenumire() == "d1");
    assert(listaFiltrata[1].getDenumire() == "d3");
}

void testFiltruSubstService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 50, "p1", "s2");
    assert(service.getSize() == 1);
    service.addMedicamentFarmacie("d2", 100, "p2", "s2");
    service.addMedicamentFarmacie("d3", 50, "p7", "s8");
    assert(service.getSize() == 3);
    vector<Medicament> listaFiltrata = service.filtruSubst("s2");
    assert(listaFiltrata.size() == 2);
    assert(service.getSize() == 3);
    assert(listaFiltrata[0].getDenumire() == "d1");
    assert(listaFiltrata[1].getDenumire() == "d2");
}

void testSortareService(){
    Repo repo;
    InFileRepo repoF("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
    Reteta reteta;
    Service service{repo, reteta};
    Service serviceF{repoF, reteta};
    assert(service.getSize() == 0);
    assert(serviceF.getSize() == 0);
    service.addMedicamentFarmacie("d2", 100, "p1", "s2");
    assert(service.getSize() == 1);
    service.addMedicamentFarmacie("d3", 50, "p2", "s2");
    service.addMedicamentFarmacie("d1", 50, "p7", "s8");
    assert(service.getSize() == 3);
    serviceF.addMedicamentFarmacie("d2", 100, "p1", "s2");
    assert(serviceF.getSize() == 1);
    serviceF.addMedicamentFarmacie("d3", 50, "p2", "s2");
    serviceF.addMedicamentFarmacie("d1", 50, "p7", "s8");
    assert(serviceF.getSize() == 3);
    Medicament m1{"d1", 100, "p1", "s1"};
    Medicament m2{"d2", 200, "p2", "s2"};
    vector<Medicament> sortate = service.sortareMedicamenteDenumire(true);
    assert(sortate.size() == 3);
    assert(sortate[0].getDenumire() == "d1");
    assert(sortate[1].getDenumire() == "d2");
    assert(sortate[2].getDenumire() == "d3");
    sortate = service.sortareMedicamenteProducator(true);
    assert(sortate[0].getProducator() == "p1");
    assert(sortate[1].getProducator() == "p2");
    assert(sortate[2].getProducator() == "p7");
    sortate = service.sortareMedicamenteSubstPret(true);
    assert(sortate[0].getSubst() == "s2" && sortate[0].getPret() == 50);
    assert(sortate[1].getSubst() == "s2" && sortate[1].getPret() == 100) ;
    assert(sortate[2].getSubst() == "s8" && sortate[2].getPret() == 50);
    vector<Medicament> sortateF = serviceF.sortareMedicamenteDenumire(false);
    assert(sortateF[0].getDenumire() == "d3");
    assert(sortateF[1].getDenumire() == "d2");
    assert(sortateF[2].getDenumire() == "d1");
    sortateF = serviceF.sortareMedicamenteProducator(false);
    assert(sortateF[0].getProducator() == "p7");
    assert(sortateF[1].getProducator() == "p2");
    assert(sortateF[2].getProducator() == "p1");
    sortateF = serviceF.sortareMedicamenteSubstPret(false);
    assert(sortateF[0].getSubst() == "s8" && sortate[2].getPret() == 50);
    assert(sortateF[2].getSubst() == "s2" && sortate[0].getPret() == 50);
    assert(sortateF[1].getSubst() == "s2" && sortate[1].getPret() == 100) ;
    clearFile("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
}

void testAddRetetaService(){
    InFileRepo repoF{"/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest"};
    Reteta reteta;
    Service service{repoF, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    service.addMedicamentFarmacie("d2", 45.25, "p2", "s2");
    assert(service.getSize() == 2);
    assert(reteta.getSize() == 0);
    service.addMedicamentReteta("d2", "p2");
    assert(reteta.getSize() == 1);
    assert(reteta.getAll()[0].getDenumire() == "d2");
    try{
        service.addMedicamentReteta("d3", "p1");
        assert(false);
    }
    catch (Error& error){
        assert(true);
        assert(error.getMesaj() == "Medicament indisponibil in farmacie!");
    }
    clearFile("/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Teste/InFileRepoTest");
}

void testDeleteAllService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    service.addMedicamentFarmacie("d2", 45.25, "p2", "s2");
    service.addMedicamentReteta("d2", "p2");
    service.addMedicamentReteta("d1", "p1");
    assert(reteta.getSize() == 2);
    service.deleteAllReteta();
    assert(reteta.getSize() == 0);
}

void testAddRandomService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    service.addMedicamentFarmacie("d1", 100, "p1", "s1");
    service.addMedicamentFarmacie("d2", 150, "p2", "s2");
    service.addMedicamentFarmacie("d3", 200, "p3", "s3");
    service.addMedicamentFarmacie("d4", 300, "p4", "s4");
    service.addMedicamenteRandom(3);
    assert(reteta.getSize() == 3);
    service.addMedicamenteRandom(5);
    assert(reteta.getSize() == 5);
    try{
        service.addMedicamenteRandom(-3);
        assert(false);
    }
    catch (Error& error){
        assert(true);
        assert(error.getMesaj() == "Numarul citit trebuie sa fie mai mare decat 0!");
    }
}

void testExportToFile(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    service.addMedicamentFarmacie("d1", 100, "p1", "s1");
    service.addMedicamentFarmacie("d2", 150, "p2", "s2");
    service.addMedicamentFarmacie("d3", 200, "p3", "s3");
    service.addMedicamentFarmacie("d4", 300, "p4", "s4");
    service.addMedicamentReteta("d4", "p4");
    service.addMedicamentReteta("d2", "p2");
    service.addMedicamentReteta("d1", "p1");
    assert(service.getSizeReteta() == 3);
    service.exportToFile("/Users/stiubedenis/Desktop/Facultate/Teme/Semestrul 2/OOP/lab_10-11(GUI)/tests/test_export");
    try{
        service.exportToFile("");
        assert(false);
    }
    catch (Error& error){
        assert(error.getMesaj() == "Numele fisierului nu poate fi gol!");
    }
}

void testUndo(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    try{
        service.undo();
        assert(false);
    }
    catch (Error& err){
        assert(err.getMesaj() == "Nu se poate face undo!");
    }
    service.addMedicamentFarmacie("d1", 100, "p1", "s1");
    assert(service.getSize() == 1);
    service.undo();
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 100, "p1", "s1");
    service.deleteMedicamentFarmacie("d1", "p1");
    assert(service.getSize() == 0);
    service.undo();
    assert(service.getSize() == 1);
    assert(service.getAll()[0].getDenumire() == "d1");
    assert(service.getAll()[0].getSubst() == "s1");
    assert(service.getAll()[0].getPret() == 100);
    service.updateSubst("d1", "p1", "s4");
    assert(service.getAll()[0].getSubst() == "s4");
    service.undo();
    assert(service.getAll()[0].getSubst() == "s1");
}

void testGetMap(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    service.addMedicamentFarmacie("d1", 100, "p1", "s1");
    service.addMedicamentFarmacie("d2", 150, "p2", "s2");
    service.addMedicamentFarmacie("d3", 200, "p2", "s3");
    service.addMedicamentFarmacie("d4", 300, "p4", "s4");
    unordered_map<string, int> dict = service.get_map();
    assert(dict.size() == 3);
    auto it = dict.find("p2");
    assert(dict.find("p10") == dict.end());
    assert(it->first == "p2" && it->second == 2);
}

void testGetAllService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    service.addMedicamentFarmacie("d2", 45.25, "p2", "s2");
    assert(service.getSize() == 2);
    vector<Medicament> med = service.getAll();
    assert(med.size() == 2);
    assert(med[0].getDenumire() == "d1");
    assert(med[1].getProducator() == "p2");
    service.addMedicamenteRandom(3);
    vector<Medicament> ret = service.getAllReteta();
    assert(service.getSizeReteta() == 3);
    assert(ret[0].getPret() == 67.23 || ret[0].getPret() == 45.25);
}

void testGetSizeService(){
    Repo repo;
    Reteta reteta;
    Service service{repo, reteta};
    assert(service.getSize() == 0);
    service.addMedicamentFarmacie("d1", 67.23, "p1", "s1");
    service.addMedicamentFarmacie("d2", 45.25, "p2", "s2");
    assert(service.getSize() == 2);
}

void testService(){
    testAddFarmacieService();
    testAddRetetaService();
    testAddRandomService();
    testDeleteFarmacieService();
    testDeleteAllService();
    testExportToFile();
    testUpdatePretService();
    testUpdateSubstService();
    testFindMedicament();
    testFiltruPretService();
    testFiltruSubstService();
    testSortareService();
    testUndo();
    testGetMap();
    testGetAllService();
    testGetSizeService();
}

void testAll() {
    testDomain();
    testRepo();
    testInFileRepo();
    testReteta();
    testService();
}

