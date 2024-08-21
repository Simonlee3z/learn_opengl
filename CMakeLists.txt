cmake_minimum_required(VERSION 3.0.0)
project(Render VERSION 0.1.0)

# 使用 C++ 17 标准
set(CMAKE_CXX_STANDARD 17)

# 设置代码文件
set(SRC_DIR ${PROJECT_SOURCE_DIR}/src/)

# 添加头文件
set(HEADER_DIR ${PROJECT_SOURCE_DIR}/include/)
set(LIB_DIR ${PROJECT_SOURCE_DIR}/lib/)
set(LIB_DIR_MAC ${LIB_DIR}mac)
set(LIB_DIR_WIN ${LIB_DIR}win)

# 添加目标链接和包含目录
if(APPLE)
    include_directories(${HEADER_DIR} ${LIB_DIR_MAC})
    set(GLFW_LINK ${LIB_DIR_MAC}/libglfw.3.dylib)
    set(OPENGL_LIB "-framework OpenGL")
elseif(WIN32)
    include_directories(${HEADER_DIR} ${LIB_DIR_WIN})
    set(GLFW_LINK ${LIB_DIR_WIN}/libglfw3dll.a)  # 使用 libglfw3dll.a 进行链接
    set(OPENGL_LIB opengl32.lib)
endif()

# 执行编译命令
set(SOURCES ${SRC_DIR}glad.c ${SRC_DIR}main.cpp)
add_executable(Render ${SOURCES})

# 链接库
target_link_libraries(Render ${GLFW_LINK} ${OPENGL_LIB})

include(CTest)
enable_testing()

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
