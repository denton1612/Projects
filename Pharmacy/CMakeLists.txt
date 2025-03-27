cmake_minimum_required(VERSION 3.28)
project(lab13_14)

set(CMAKE_CXX_STANDARD 17)

# Set Qt Path
set(Qt6_DIR "/Users/stiubedenis/Downloads/Qt/6.5.3/macos/lib/cmake/Qt6")

# Find Qt
find_package(Qt6 REQUIRED COMPONENTS Core Gui Widgets)

# Enable Qt's automatic processing
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

add_executable(lab13_14
        main.cpp
        Domain/Medicament.cpp
        Domain/Medicament.h
        Repository/RepoAbstract.cpp
        Repository/RepoAbstract.h
        Repository/Repo.cpp
        Repository/Repo.h
        Repository/InFileRepo.cpp
        Repository/InFileRepo.h
        Validare/Error.cpp
        Validare/Error.h
        Teste/teste.cpp
        Teste/teste.h
        Utils/utils.cpp
        Utils/utils.h
        Service/Service.cpp
        Service/Service.h
        Reteta/Reteta.cpp
        Reteta/Reteta.h
        Undo/ActiuneUndo.cpp
        Undo/ActiuneUndo.h
        Observer/Observer.cpp
        Observer/Observer.h
        GUI/FarmacieGUI.cpp
        GUI/FarmacieGUI.h
        GUI/RetetaGUI.cpp
        GUI/RetetaGUI.h
        GUI/RetetaReadOnlyGUI.cpp
        GUI/RetetaReadOnlyGUI.h
        ListModel/ListModel.cpp
        ListModel/ListModel.h
)

target_link_libraries(lab13_14 Qt::Core Qt::Gui Qt::Widgets)